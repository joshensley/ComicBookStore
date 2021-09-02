using ComicBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories
{
    public interface IProductInventoryRepository
    {
        Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<Product, TResult>> selector);

        Task<ActionResult<TResult>> GetByProductId<TResult>(int id, Expression<Func<Product, TResult>> selector);

        Task<ActionResult<TResult>> GetInventoryItemById<TResult>(int id, Expression<Func<ProductInventory, TResult>> selector);

        Task<ActionResult<bool>> PostRange(List<ProductInventory> productInventoryRange);

        Task<ActionResult<bool>> Edit(ProductInventory productInventory);

        Task<ActionResult<bool>> Delete(int id);
    }
}
