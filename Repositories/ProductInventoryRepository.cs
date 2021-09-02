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
    public class ProductInventoryRepository : IProductInventoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductInventoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<Product, TResult>> selector)
        {
            var productInventory = await _db.Products
                .Include(p => p.ProductType)
                .Include(p => p.CategoryType)
                .Include(p => p.ProductInventory.Where(p => p.InStock == true))
                .Select(selector)
                .ToListAsync();

            return productInventory;
        }

        public async Task<ActionResult<TResult>> GetByProductId<TResult>(int id, Expression<Func<Product, TResult>> selector)
        {
            var productInventory = await _db.Products
                .Where(p => p.ID == id)
                .Include(p => p.ProductType)
                .Include(p => p.CategoryType)
                .Include(p => p.ProductInventory.OrderBy(p => p.InStock))
                .Select(selector)
                .FirstOrDefaultAsync();

            return productInventory;
        }

        public async Task<ActionResult<TResult>> GetInventoryItemById<TResult>(int id, Expression<Func<ProductInventory, TResult>> selector)
        {
            var productInventory = await _db.ProductInventory
                .Where(p => p.ID == id)
                .Select(selector)
                .FirstOrDefaultAsync();

            return productInventory;
        }

        public async Task<ActionResult<bool>> PostRange(List<ProductInventory> productInventoryRange)
        {
            _db.ProductInventory.AddRange(productInventoryRange);
            await _db.SaveChangesAsync();
            return true;

        }

        public async Task<ActionResult<bool>> Edit(ProductInventory productInventory)
        {
            _db.ProductInventory.Update(productInventory);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ActionResult<bool>> Delete(int id)
        {
            var productInventory = await _db.ProductInventory.FindAsync(id);
            _db.ProductInventory.Remove(productInventory);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
