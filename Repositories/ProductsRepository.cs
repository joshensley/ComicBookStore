using ComicBookStore.Data;
using ComicBookStore.Models;
using ComicBookStore.Repositories.DTO;
using ComicBookStore.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<Product, TResult>> selector)
        {
            var products = await _db.Products
                .OrderBy(p => p.Name)
                .Include(p => p.ProductSpecificationValues)
                    .ThenInclude(p => p.ProductSpecification)
                .Include(p => p.ProductImages)
                .Select(selector)
                .ToListAsync();

            return products;
        }

        public async Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<Product, TResult>> selector)
        {
            var product = await _db.Products
                .Where(p => p.ID == id)
                .Include(p => p.ProductSpecificationValues)
                    .ThenInclude(p => p.ProductSpecification)
                .Include(p => p.ProductImages)
                .Select(selector)
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<ActionResult<PaginatedList<TResult>>> GetByProductTypeId<TResult>(
            int id,
            Expression<Func<Product, TResult>> selector,
            int? pageNumber)
        {
            if (pageNumber == null)
            {
                IQueryable<TResult> products = _db.Products
                    .Where(p => p.ProductTypeID == id)
                    .Select(selector);

                var fullPageSize = await _db.Products
                    .Where(p => p.ProductTypeID == id)
                    .CountAsync();
                
                return await PaginatedList<TResult>.CreateAsync(products, pageNumber ?? 1, fullPageSize); ;
            }

            IQueryable<TResult> productsIQ = _db.Products
                .Where(p => p.ProductTypeID == id)
                .Select(selector);

            int pageSize = 3;
            return await PaginatedList<TResult>.CreateAsync(productsIQ, pageNumber ?? 1, pageSize);
           
        }

        public async Task<ActionResult<Product>> Post(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<ActionResult<Product>> Edit(Product product)
        {
            _db.Products.UpdateRange(product);
            await _db.SaveChangesAsync();
            return product;
        }

    }
}
