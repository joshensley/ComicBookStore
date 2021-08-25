using ComicBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories
{
    public interface ICategoryTypeRepository
    {
        Task<ActionResult<IEnumerable<TResult>>> Get<TResult>(Expression<Func<CategoryType, TResult>> selector);

        Task<ActionResult<TResult>> GetById<TResult>(int id, Expression<Func<CategoryType, TResult>> selector);

        Task<ActionResult<bool>> Post(CategoryType categoryType);

        Task<ActionResult<bool>> Edit(CategoryType categoryType);

        Task<ActionResult<bool>> Delete(int id);
    }
}
