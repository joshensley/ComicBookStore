using ComicBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories
{
    public interface IProductTypeRepository
    {
        Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<ProductType, TResult>> selector);

        Task<ActionResult<IEnumerable<TResult>>> GetWithProductSpecifications<TResult>(Expression<Func<ProductType, TResult>> selector);

        Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<ProductType, TResult>> selector);

        Task<ActionResult<bool>> Post(ProductType productType);

        Task<ActionResult<bool>> Edit(ProductType productType);

        Task<ActionResult<bool>> Delete(int id);
    }
}
