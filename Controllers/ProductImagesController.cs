using ComicBookStore.Data;
using ComicBookStore.Models;
using ComicBookStore.Models.ViewModels;
using ComicBookStore.Services;
using ComicBookStore.Utility;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComicBookStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductImagesController : Controller
    {
        public readonly IWebHostEnvironment _environment;
        public readonly ProductsService _productsService;
        public readonly ApplicationDbContext _db;

        public ProductImagesController(
            IWebHostEnvironment environment, 
            ProductsService productsService,
            ApplicationDbContext db)
        {
            _environment = environment;
            _productsService = productsService;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var products = (await _productsService.GetProductDetailDTO()).Value;

            return View(products);
        }

        public async Task<IActionResult> Create(int id)
        {
            var product = (await _productsService.GetByIdProductDetailDTO(id)).Value;

            if (product == null) return NotFound();

            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseKeys.ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseKeys.AuthEmail, FirebaseKeys.AuthPassword);

            List<string> productImagesURL = new List<string>();
            foreach (var item in product.ProductImages)
            {
                var task = new FirebaseStorage(
                    FirebaseKeys.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("images")
                    .Child($"{item.ImageFileName}")
                    .GetDownloadUrlAsync().Result;

                productImagesURL.Add(task);
            }

            ProductImageViewModel productImageViewModel = new ProductImageViewModel()
            {
                ProductDTO = product,
                ProductID = product.ID,
                ProductImagesURL = productImagesURL
            };

            return View(productImageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,FileUpload")] ProductImageViewModel productImageViewModel)
        {

            FileStream fs;
            var fileUpload = productImageViewModel.FileUpload;
            var folderName = "firebase";
            var productID = productImageViewModel.ProductID;
            string fileName = Guid.NewGuid() + "_" + fileUpload.FileName;
            string filePath = Path.Combine(_environment.WebRootPath, $"images/{folderName}", fileName);
            string fileImagePath = Path.Combine(_environment.ContentRootPath, "wwwroot/images/firebase/", fileName);

            if (ModelState.IsValid && fileUpload.Length > 0)
            {
                // upload file to static image
                using (var fileStream = new FileStream(fileImagePath, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(fileStream);
                }

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
                    .Child("images")
                    .Child($"{fileName}")
                    .PutAsync(fs, cancellation.Token);

                await upload;

                ProductImage productImage = new ProductImage()
                {
                    ProductID = productID,
                    ImageFileName = fileName,
                    IsFeature = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _db.Add(productImage);
                await _db.SaveChangesAsync();

                fs.Close();
                System.IO.File.Delete(filePath);

                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = (await _productsService.GetByIdProductDetailDTO(id)).Value;

            if (product == null) return NotFound();

            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseKeys.ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseKeys.AuthEmail, FirebaseKeys.AuthPassword);

            var productImagesURL = new List<ImageUrlWithID>();
            foreach (var item in product.ProductImages)
            {
                var task = new FirebaseStorage(
                    FirebaseKeys.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("images")
                    .Child($"{item.ImageFileName}")
                    .GetDownloadUrlAsync().Result;

                var productImage = new ImageUrlWithID()
                {
                    ID = item.ID,
                    URL = task,
                    FileName = item.ImageFileName
                };

                productImagesURL.Add(productImage);
            }

            ProductImageViewModel productImageViewModel = new ProductImageViewModel()
            {
                ProductDTO = product,
                ProductID = product.ID,
                ImageUrlWithID = productImagesURL
            };

            return View(productImageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<List<int>> Delete(List<int> productImageIds)
        {
            var productImages = await _db.ProductImages
                .Where(p => productImageIds.Contains(p.ID))
                .ToListAsync();

            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseKeys.ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseKeys.AuthEmail, FirebaseKeys.AuthPassword);

            foreach (var item in productImages)
            {
                var delete = new FirebaseStorage(
                    FirebaseKeys.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("images")
                    .Child($"{item.ImageFileName}")
                    .DeleteAsync();

                await delete;
            }

            _db.ProductImages.RemoveRange(productImages);
            await _db.SaveChangesAsync();
          

            return productImageIds;
        }
    }
}
