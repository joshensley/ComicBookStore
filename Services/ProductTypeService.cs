using ComicBookStore.Models;
using ComicBookStore.Repositories;
using ComicBookStore.Repositories.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Services
{
    public class ProductTypeService
    {
        public readonly IProductTypeRepository _productTypeRepository;

        public ProductTypeService(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        // GET: Get all product types
        public async Task<ActionResult<IEnumerable<ProductTypeDTO>>> GetProductTypeDTO()
        {
            return await _productTypeRepository.Get(ProductTypeDTO.ProductTypeSelector);
        }

        // GET: Get product type details
        public async Task<ActionResult<ProductTypeDTO>> GetByIdProductTypeDTO(int id)
        {
            return await _productTypeRepository.GetById(id, ProductTypeDTO.ProductTypeSelector);
        }

        // POST: Post product type
        public async Task<ActionResult<bool>> Post(ProductType productType)
        {
            return await _productTypeRepository.Post(productType);
        }

        // EDIT: Edit product type
        public async Task<ActionResult<bool>> Edit(ProductType productType)
        {
            return await _productTypeRepository.Edit(productType);
        }

        // DELETE: Delete product type
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _productTypeRepository.Delete(id);
        }
    }
}
