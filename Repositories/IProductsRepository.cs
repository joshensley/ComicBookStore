using ComicBookStore.Models;
using ComicBookStore.Repositories.DTO;
using ComicBookStore.Utility;
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

        Task<ActionResult<PaginatedList<TResult>>> GetByProductTypeId<TResult>(
            int id,
            Expression<Func<Product, TResult>> selector,
            int? pageNumber);

        Task<ActionResult<Product>> Post(Product product);

        Task<ActionResult<Product>> Edit(Product product);
    }
}
