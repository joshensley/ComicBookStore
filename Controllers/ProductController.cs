using ComicBookStore.Services;
using ComicBookStore.Utility;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductsService _productsService;

        public ProductController(ProductsService productsService)
        {
            _productsService = productsService;
        }
            
        public async Task<IActionResult> Index(int id, int pageNumber)
        {
            var product = (await _productsService.GetProductDTOByProductTypeID(
                id: id,
                pageNumer: pageNumber
                ));

            var auth = new FirebaseAuthProvider(new FirebaseConfig(FirebaseKeys.ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(FirebaseKeys.AuthEmail, FirebaseKeys.AuthPassword);

            foreach (var productItem in product.Value)
            {
                if (productItem.ProductImages.Count() > 0)
                {
                    foreach (var item in productItem.ProductImages)
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
                            .GetDownloadUrlAsync()
                            .Result;

                        item.ImageUrl = task;
                    }
                }
            }
    
            return View(product.Value);
        }
    }
}
