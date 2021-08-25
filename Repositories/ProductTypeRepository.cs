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
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<ProductType, TResult>> selector)
        {
            var productTypes = await _db.ProductTypes
                .OrderBy(x => x.Name)
                .Select(selector)
                .ToListAsync();

            return productTypes;
        }

        public async Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<ProductType, TResult>> selector)
        {
            var productType = await _db.ProductTypes
                .Where(i => i.ID == id)
                .Select(selector)
                .FirstOrDefaultAsync();

            return productType;
        }

        public async Task<ActionResult<bool>> Post(ProductType productType)
        {
            _db.ProductTypes.Add(productType);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ActionResult<bool>> Edit(ProductType productType)
        {
            _db.ProductTypes.Update(productType);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ActionResult<bool>> Delete(int id)
        {
            var productType = await _db.ProductTypes.FindAsync(id);
            _db.ProductTypes.Remove(productType);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
