using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ComicBookStore.Data;
using ComicBookStore.Models;
using ComicBookStore.Models.ViewModels;
using ComicBookStore.Utility;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ComicBookStore.Areas.Identity.Pages.Account.Manage
{
    public class ProfilePictureModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public ProfilePictureModel(
            ApplicationDbContext db, 
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            _db = db;
            _userManager = userManager;
            _environment = environment;
        }

        [BindProperty]
        public string ApplicationUserID { get; set; }

        [BindProperty]
        public List<ProfileImageViewModel> ProfileImageViewModel { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string ApplicationUserID { get; set; }
            public IFormFile FileUpload { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationUserID = (await _userManager.GetUserAsync(User)).Id;

            var profileImages = await _db.ProfileImages.Where(p => p.ApplicationUserID == ApplicationUserID).ToListAsync();

            if (profileImages != null)
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseKeys.ApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseKeys.AuthEmail, FirebaseKeys.AuthPassword);

                List<ProfileImageViewModel> profileImageViewModels = new List<ProfileImageViewModel>();
                foreach (var item in profileImages)
                {
                    var task = new FirebaseStorage(
                        FirebaseKeys.Bucket,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                            ThrowOnCancel = true
                        })
                        .Child("profile")
                        .Child($"{item.ImageFileName}")
                        .GetDownloadUrlAsync().Result;

                    var profileImageViewModel = new ProfileImageViewModel()
                    {
                        ProfileImageID = item.ID,
                        IsProfileImage = item.IsProfileImage,
                        ImageFileName = item.ImageFileName,
                        ApplicationUserID = item.ApplicationUserID,
                        URL = task
                    };

                    profileImageViewModels.Add(profileImageViewModel);
                }

                ProfileImageViewModel = profileImageViewModels;
            } 

            return Page();
        }

        public async Task<IActionResult> OnPostProfileImage([Bind("ApplicationUserID,FileUpload")] InputModel input)
        {
            FileStream fs;
            var fileUpload = input.FileUpload;
            var folderName = "firebase";
            var applicationUserID = input.ApplicationUserID;
            string fileName = Guid.NewGuid() + "_" + fileUpload.FileName;
            string filePath = Path.Combine(_environment.WebRootPath, $"images/{folderName}", fileName);
            string fileImagePath = Path.Combine(_environment.ContentRootPath, "wwwroot/images/firebase/", fileName);

            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            if (ModelState.IsValid && fileUpload.Length > 0)
            {
                // upload file to static image
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(fileStream);
                };

                // Upload static image to firebase
                fs = new FileStream(filePath, FileMode.Open);
                var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseKeys.ApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseKeys.AuthEmail, FirebaseKeys.AuthPassword);
                var cancellation = new CancellationTokenSource();

                var upload = new FirebaseStorage(
                    FirebaseKeys.Bucket,
                    new FirebaseStorageOptions 
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("profile")
                    .Child($"{fileName}")
                    .PutAsync(fs, cancellation.Token);

                await upload;

                ProfileImage profileImage = new ProfileImage()
                {
                    ApplicationUserID = applicationUserID,
                    ImageFileName = fileName,
                    IsProfileImage = true,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                _db.Add(profileImage);
                await _db.SaveChangesAsync();

                fs.Close();
                System.IO.File.Delete(filePath);

                // turn other IsProfileImage properties for the user to false 
                var profileImages = _db.ProfileImages
                    .Where(p => p.ApplicationUserID == applicationUserID 
                        && p.IsProfileImage == true 
                        && p.ImageFileName != fileName)
                    .FirstOrDefault();

                if (profileImages != null)
                {
                    profileImages.IsProfileImage = false;
                    _db.Update(profileImages);
                    await _db.SaveChangesAsync();
                }

                return RedirectToPage("ProfilePicture");

            }

            return RedirectToPage("ProfilePicture");
        }

        public async Task<IActionResult> OnPostUpdateProfileImage(string id)
        {
            var oldProfileImage = await _db.ProfileImages
                .Where(p => p.IsProfileImage == true)
                .FirstOrDefaultAsync();

            var newProfileImage = await _db.ProfileImages
                .Where(p => p.ImageFileName == id)
                .FirstOrDefaultAsync();

            oldProfileImage.IsProfileImage = false;
            newProfileImage.IsProfileImage = true;

            _db.ProfileImages.Update(oldProfileImage);
            _db.ProfileImages.Update(newProfileImage);
            await _db.SaveChangesAsync();

            return RedirectToPage("ProfilePicture");
        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            var profileImage = await _db.ProfileImages
                .Where(p => p.ImageFileName == id)
                .FirstOrDefaultAsync();

            if (profileImage != null)
            {
                // Delete image in Firebase
                var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseKeys.ApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseKeys.AuthEmail, FirebaseKeys.AuthPassword);
                var cancellation = new CancellationTokenSource();

                var delete = new FirebaseStorage(
                    FirebaseKeys.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("profile")
                    .Child($"{id}")
                    .DeleteAsync();

                await delete;

                // Delete from database
                _db.ProfileImages.Remove(profileImage);
                await _db.SaveChangesAsync();

                // If deleted image was profile picture change an image to new profile picture
                if (profileImage.IsProfileImage == true)
                {
                    var newMainProfileImage = await _db.ProfileImages
                        .Where(p => p.ApplicationUserID == profileImage.ApplicationUserID)
                        .FirstOrDefaultAsync();

                    if (newMainProfileImage != null)
                    {
                        newMainProfileImage.IsProfileImage = true;
                        _db.Update(newMainProfileImage);
                        await _db.SaveChangesAsync();
                    }
                }
                
            }

            return RedirectToPage("ProfilePicture");

        }
    }
}
