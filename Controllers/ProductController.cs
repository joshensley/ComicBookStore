using ComicBookStore.Models;
using ComicBookStore.Repositories.DTO;
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
        private readonly CartService _cartService;

        public ProductController(ProductsService productsService, CartService cartService)
        {
            _productsService = productsService;
            _cartService = cartService;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<List<CartItemDTO>> AddToCart(int id, string UserId)
        {
            // get current user cart
            var cartItems = (await _cartService.GetCartItemsDTO(UserId)).Value;

            // check if product ID is already in user cart
            // if ID in user cart skip posting item and then return current cart
            foreach (var item in cartItems)
            {
                if (item.ProductID == id)
                {
                    return cartItems;
                }
            }

            // if ID not in user cart, post item to cart
            var cartItem = new Cart()
            {
                ApplicationUserID = UserId,
                ProductID = id,
                Quantity = 1
            };

            await _cartService.Post(cartItem);

            // get new current user cart
            var newCartItems = (await _cartService.GetCartItemsDTO(UserId)).Value;

            return newCartItems;
        }
    }
}
