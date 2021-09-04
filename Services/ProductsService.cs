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
    public class ProductsService
    {
        public readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        // GET: Get all products
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductDTO()
        {
            return await _productsRepository.Get(ProductDTO.ProductSelector);
        }

        // GET: Get all products with details
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductDetailDTO()
        {
            return await _productsRepository.Get(ProductDTO.ProductDetailSelector);
        }

        // GET: Get product by id with details
        public async Task<ActionResult<ProductDTO>> GetByIdProductDetailDTO(int id)
        {
            return await _productsRepository.GetById(id, ProductDTO.ProductDetailSelector);
        }

        // GET: Get all products by ProductTypeID
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductDTOByProductTypeID(int id)
        {
            return await _productsRepository.GetByProductTypeId(id, ProductDTO.ProductSelector);
        }

        // POST: Post product
        public async Task<ActionResult<Product>> Post(Product product)
        {
            return await _productsRepository.Post(product);
        }

        // POST: Edit product
        public async Task<ActionResult<Product>> Edit(Product product)
        {
            return await _productsRepository.Edit(product);
        }
    }
}
