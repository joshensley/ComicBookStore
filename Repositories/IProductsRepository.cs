using ComicBookStore.Models;
using ComicBookStore.Repositories.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories
{
    public interface IProductsRepository
    {
        Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<Product, TResult>> selector);

        Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<Product, TResult>> selector);

        Task<ActionResult<IEnumerable<TResult>>> GetByProductTypeId<TResult>(int id, Expression<Func<Product, TResult>> selector);

        Task<ActionResult<Product>> Post(Product product);

        Task<ActionResult<Product>> Edit(Product product);
    }
}
