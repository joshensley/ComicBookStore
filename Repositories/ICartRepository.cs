using ComicBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories
{
    public interface ICartRepository
    {
        Task<ActionResult<List<TResult>>> Get<TResult>(string userId, Expression<Func<Cart, TResult>> selector);

        Task<ActionResult<Cart>> Post(Cart cartItem);

    }
}
