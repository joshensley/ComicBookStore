using ComicBookStore.Models;
using ComicBookStore.Models.ViewModels;
using ComicBookStore.Repositories;
using ComicBookStore.Repositories.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Services
{
    public class ProductInventoryService
    {
        public readonly IProductInventoryRepository _productInventoryRepository;

        public ProductInventoryService(IProductInventoryRepository productInventoryRepository)
        {
            _productInventoryRepository = productInventoryRepository;
        }

        // GET: Get all products with inventory 
        public async Task<ActionResult<IEnumerable<ProductInventoryDTO>>> GetProductInventoryDTO()
        {
            return await _productInventoryRepository.Get(ProductInventoryDTO.ProductInventorySelector);
        }

        // GET: Get a single product with inventory by product id
        public async Task<ActionResult<ProductInventoryDTO>> GetProductInventoryByProductIdDTO(int id)
        {
            return await _productInventoryRepository.GetByProductId(id, ProductInventoryDTO.ProductInventorySelector);
        }

        // GET: Get a single inventory item
        public async Task<ActionResult<InventoryItemDTO>> GetInventoryItemDTO(int id)
        {
            return await _productInventoryRepository.GetInventoryItemById(id, InventoryItemDTO.ProductInventorySelector);
        }

        // POST: Post range of inventory
        public async Task<ActionResult<bool>> PostRange(CreateProductInventoryViewModel model)
        {
            var productInventoryRange = new List<ProductInventory>();

            for (int i = 0; i < model.Quantity; i++)
            {
                var productInventory = new ProductInventory()
                {
                    ID = model.ID,
                    InStock = model.InStock,
                    IsActive = model.IsActive,
                    ProductID = model.ProductID,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                productInventoryRange.Add(productInventory);
            }

            return await _productInventoryRepository.PostRange(productInventoryRange);
        }

        // EDIT: Edit single inventory item
        public async Task<ActionResult<bool>> Edit(CreateProductInventoryViewModel model)
        {
            var productInventory = new ProductInventory()
            {
                ID = model.ID,
                InStock = model.InStock,
                IsActive = model.IsActive,
                ProductID = model.ProductID,
                CreatedAt = model.CreatedAt,
                UpdatedAt = DateTime.Now
            };

            return await _productInventoryRepository.Edit(productInventory);
        }

        // DELETE: Delete inventory item
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _productInventoryRepository.Delete(id);
        }
    }
}
