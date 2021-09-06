using ComicBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories
{
    public interface IProductSpecificationValueRepository
    {
        Task<ActionResult<bool>> Post(ProductSpecificationValue productSpecificationValue);

        Task<ActionResult<bool>> PostRange(List<ProductSpecificationValue> productSpecificationValues);

        Task<ActionResult<bool>> Edit(ProductSpecificationValue productSpecificationValue);

        Task<ActionResult<bool>> DeleteByProductSpecificationID(int id);
    }
}
