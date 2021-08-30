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
    public class ProductSpecificationRepository : IProductSpecificationRepository
    {
        public readonly ApplicationDbContext _db;

        public ProductSpecificationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<IEnumerable<TResult>>> GetByProductTypeId<TResult>(int id, Expression<Func<ProductSpecification, TResult>> selector)
        {
            var productSpecifications = await _db.ProductSpecification
                .Where(p => p.ProductTypeID == id)
                .Select(selector)
                .ToListAsync();

            return productSpecifications;
        }
    }
}
