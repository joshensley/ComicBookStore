using ComicBookStore.Models;
using ComicBookStore.Repositories;
using ComicBookStore.Repositories.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Services
{
    public class CartService
    {
        public readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // GET: Get all product items in cart
        public async Task<ActionResult<List<CartItemDTO>>> GetCartItemsDTO(string userId)
        {
            return await _cartRepository.Get(userId, CartItemDTO.CartItemSelector);
        }

        // POST: Post product item in cart
        public async Task<ActionResult<Cart>> Post(Cart cartItem)
        {
            return await _cartRepository.Post(cartItem);
        }
    }
}
