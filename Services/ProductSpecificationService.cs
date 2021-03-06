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

        // POST: Post product specification range
        public async Task<ActionResult<IEnumerable<ProductSpecification>>> PostProductSpecificationRange(IList<ProductSpecification> productSpecifications)
        {
            var productSpecificationsRange = new List<ProductSpecification>();

            foreach (var item in productSpecifications)
            {
                var productSpecification = new ProductSpecification()
                {
                    Name = item.Name,
                    ProductTypeID = item.ProductTypeID
                };

                productSpecificationsRange.Add(productSpecification);
            }

            return await _productSpecificationRepository.PostRange(productSpecificationsRange);
        }

        // EDIT: Edit Product Specification Range
        public async Task<ActionResult<IEnumerable<ProductSpecification>>> EditProductSpecificationsRange(IList<ProductSpecification> productSpecifications)
        {
            var productSpecificationsRange = productSpecifications.ToList();

            return await _productSpecificationRepository.EditRange(productSpecificationsRange);
        }

        // DELETE: Delete product specification
        public async Task<ActionResult<bool>> DeleteProductSpecificationByID(int id)
        {
            return await _productSpecificationRepository.Delete(id);
        }

        // POST: Create product specificaiton value range
        public List<ProductSpecificationValue> CreateProductSpecificationValueList(
            IEnumerable<ProductSpecification> productSpecifications, 
            IEnumerable<ProductDTO> products)
        {

            var productSpecificationValues = new List<ProductSpecificationValue>();

            foreach (var product in products)
            {
                foreach (var productSpecification in productSpecifications)
                {
                    var productSpecificationValue = new ProductSpecificationValue()
                    {
                        Value = "",
                        ProductSpecificationID = productSpecification.ID,
                        ProductID = product.ID
                    };

                    productSpecificationValues.Add(productSpecificationValue);
                }
            }

            return productSpecificationValues;
        }

        
    }
}
