using ComicBookStore.Repositories;
using ComicBookStore.Repositories.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Services
{
    public class ProductSpecificationService
    {
        public readonly IProductSpecificationRepository _productSpecificationRepository;

        public ProductSpecificationService(IProductSpecificationRepository productSpecificationRepository)
        {
            _productSpecificationRepository = productSpecificationRepository;
        }

        // GET: Get product specifications by product type id
        public async Task<ActionResult<IEnumerable<ProductSpecificationDTO>>> GetProductSpecificationDTOByProductTypeID(int id)
        {
            return await _productSpecificationRepository.GetByProductTypeId(id, ProductSpecificationDTO.ProductSpecificationSelector);
        }
    }
}
