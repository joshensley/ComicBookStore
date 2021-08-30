using ComicBookStore.Data;
using ComicBookStore.Models;
using ComicBookStore.Repositories.DTO;
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
                .Include(p => p.ProductSpecificationValues)
                    .ThenInclude(p => p.ProductSpecification)
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
                .Select(selector)
                .FirstOrDefaultAsync();

            return product;
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

        /*public async Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<ProductViewModel, TResult>> selector)
        {
            var products = await _db.Products
               .Select(selector)
               .ToListAsync();

            return products;
        }*/
    }
}
