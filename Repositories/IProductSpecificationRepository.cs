using ComicBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories
{
    public interface IProductSpecificationRepository
    {
        Task<ActionResult<IEnumerable<TResult>>> GetByProductTypeId<TResult>(int id, Expression<Func<ProductSpecification, TResult>> selector);

        Task<ActionResult<IEnumerable<ProductSpecification>>> PostRange(List<ProductSpecification> productSpecifications);
    }
}
