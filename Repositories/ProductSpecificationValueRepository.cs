using ComicBookStore.Data;
using ComicBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories
{
    public class ProductSpecificationValueRepository : IProductSpecificationValueRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductSpecificationValueRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<bool>> Post(ProductSpecificationValue productSpecificationValue)
        {
            _db.ProductSpecificationValues.Add(productSpecificationValue);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ActionResult<bool>> PostRange(List<ProductSpecificationValue> productSpecificationValues)
        {
            _db.ProductSpecificationValues.AddRange(productSpecificationValues);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ActionResult<bool>> Edit(ProductSpecificationValue productSpecificationValue)
        {
            _db.ProductSpecificationValues.Update(productSpecificationValue);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ActionResult<bool>> DeleteByProductSpecificationID(int id)
        {
            var productSpecificationValuesList = await _db.ProductSpecificationValues
                .Where(p => p.ProductSpecificationID == id)
                .ToListAsync();

            if (productSpecificationValuesList == null)
            {
                return false;
            }

            _db.ProductSpecificationValues.RemoveRange(productSpecificationValuesList);
            await _db.SaveChangesAsync();
            return true;

        }
    }
}
