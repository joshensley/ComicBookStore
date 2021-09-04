using ComicBookStore.Models;
using ComicBookStore.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Services
{
    public class ProductSpecificationValueService
    {
        public readonly IProductSpecificationValueRepository _productSpecificationValueRepository;

        public ProductSpecificationValueService(IProductSpecificationValueRepository productSpecificationValueRepository)
        {
            _productSpecificationValueRepository = productSpecificationValueRepository;
        }

        // POST: Post product specification value
        public async Task<ActionResult<bool>> Post(ProductSpecificationValue productSpecificationValue)
        {
            return await _productSpecificationValueRepository.Post(productSpecificationValue);
        }

        // POST: Post Product specification value range
        public async Task<ActionResult<bool>> PostRange(List<ProductSpecificationValue> productSpecificationValues)
        {
            return await _productSpecificationValueRepository.PostRange(productSpecificationValues);
        }

        // POST: Edit product specification value
        public async Task<ActionResult<bool>> Edit(ProductSpecificationValue productSpecificationValue)
        {
            return await _productSpecificationValueRepository.Edit(productSpecificationValue);
        }
    }
}
