using ComicBookStore.Data;
using ComicBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories
{
    public class CartRepository : ICartRepository
    {
        public readonly ApplicationDbContext _db;

        public CartRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<List<TResult>>> Get<TResult>(string userId, Expression<Func<Cart, TResult>> selector)
        {
            var cartItems = await _db.Cart
                .Where(c => c.ApplicationUserID == userId)
                .Include(c => c.Product)
                .Select(selector)
                .ToListAsync();

            return cartItems;
        }

        public async Task<ActionResult<Cart>> Post(Cart cartItem)
        {
            _db.Cart.Add(cartItem);
            await _db.SaveChangesAsync();
            return cartItem;
        }
    }
}
