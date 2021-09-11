using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComicBookStore.Data;
using ComicBookStore.Models;
using ComicBookStore.Services;
using Microsoft.AspNetCore.Authorization;

namespace ComicBookStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductTypeService _productTypeService;

        public ProductTypesController(ApplicationDbContext context, ProductTypeService productTypeService)
        {
            _context = context;
            _productTypeService = productTypeService;
        }

        // GET: ProductTypes
        public async Task<IActionResult> Index()
        {
            var productTypes = (await _productTypeService.GetProductTypeDTO()).Value;
            return View(productTypes);
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var productType = (await _productTypeService.GetByIdProductTypeDTO(id)).Value;

            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,IsActive")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                var response = await _productTypeService.Post(productType);
                if (response.Value) return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var productType = (await _productTypeService.GetByIdProductTypeDTO(id)).Value;

            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,IsActive")] ProductType productType)
        {
            if (id != productType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productTypeService.Edit(productType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productType.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var productType = (await _productTypeService.GetByIdProductTypeDTO(id)).Value;

            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productTypeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTypeExists(int id)
        {
            return _context.ProductTypes.Any(e => e.ID == id);
        }
    }
}
