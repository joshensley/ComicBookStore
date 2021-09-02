using ComicBookStore.Data;
using ComicBookStore.Models;
using ComicBookStore.Models.ViewModels;
using ComicBookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Controllers
{
    public class ProductInventoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ProductInventoryService _productInventoryService;
        private readonly ProductsService _productsService;

        public ProductInventoryController(
            ApplicationDbContext db,
            ProductInventoryService productInventoryService, 
            ProductsService productsService)
        {
            _db = db;
            _productInventoryService = productInventoryService;
            _productsService = productsService;
        }

        public async Task<IActionResult> Index()
        {
            var productInventoryDTO = (await _productInventoryService.GetProductInventoryDTO()).Value;

            return View(productInventoryDTO);
        }

        public async Task<IActionResult> Details(int id)
        {
            var productInventoryDTO = (await _productInventoryService.GetProductInventoryByProductIdDTO(id)).Value;

            return View(productInventoryDTO);
        }

        public async Task<IActionResult> Create()
        {
            var product = (await _productsService.GetProductDTO()).Value;

            var createProductInventoryViewModel = new CreateProductInventoryViewModel()
            {
                Products = product
            };

            return View(createProductInventoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,ProductID,InStock,IsActive")] CreateProductInventoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productInventoryService.PostRange(model);

                if (response.Value) return RedirectToAction(nameof(Index));
            }

            var product = (await _productsService.GetProductDTO()).Value;
            var createProductInventoryViewModel = new CreateProductInventoryViewModel()
            {
                Products = product
            };

            return View(createProductInventoryViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var inventoryItem = (await _productInventoryService.GetInventoryItemDTO(id)).Value;

            if (inventoryItem == null) return NotFound();

            var createProductInventoryViewModel = new CreateProductInventoryViewModel()
            {
                ID = inventoryItem.ID,
                InStock = inventoryItem.InStock,
                IsActive = inventoryItem.IsActive,
                CreatedAt = inventoryItem.CreatedAt,
                UpdatedAt = inventoryItem.UpdatedAt,
                ProductID = inventoryItem.ProductID
            };

            return View(createProductInventoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Quantity,IsActive,InStock,CreatedAt,UpdatedAt,ProductID")] CreateProductInventoryViewModel model)
        {
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productInventoryService.Edit(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInventoryExists(model.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = model.ProductID });
            }
            return View(model);
        }

        // GET: ProductInventory/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var productInventory = (await _productInventoryService.GetInventoryItemDTO(id)).Value;

            if (productInventory == null)
            {
                return NotFound();
            }

            return View(productInventory);
        }

        // POST: ProductInventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productInventoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInventoryExists(int id)
        {
            return _db.ProductInventory.Any(e => e.ID == id);
        }
    }
}
