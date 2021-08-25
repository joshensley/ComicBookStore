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

namespace ComicBookStore.Controllers
{
    public class CategoryTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CategoryTypeService _categoryTypeService;

        public CategoryTypesController(ApplicationDbContext context, CategoryTypeService categoryTypeService)
        {
            _context = context;
            _categoryTypeService = categoryTypeService;
        }

        // GET: CategoryTypes
        public async Task<IActionResult> Index()
        {
            var categoryType = (await _categoryTypeService.GetCategoryTypeDTO()).Value;
            return View(categoryType);
        }

        // GET: CategoryTypes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var categoryType = (await _categoryTypeService.GetByIdCategoryTypeDTO(id)).Value;

            if (categoryType == null)
            {
                return NotFound();
            }

            return View(categoryType);
        }

        // GET: CategoryTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,IsActive")] CategoryType categoryType)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoryTypeService.Post(categoryType);
                if(response.Value) return RedirectToAction(nameof(Index));
            }
            return View(categoryType);
        }

        // GET: CategoryTypes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var categoryType = (await _categoryTypeService.GetByIdCategoryTypeDTO(id)).Value;
            if (categoryType == null)
            {
                return NotFound();
            }
            return View(categoryType);
        }

        // POST: CategoryTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,IsActive")] CategoryType categoryType)
        {
            if (id != categoryType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryTypeService.Edit(categoryType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryTypeExists(categoryType.ID))
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
            return View(categoryType);
        }

        // GET: CategoryTypes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var categoryType = (await _categoryTypeService.GetByIdCategoryTypeDTO(id)).Value;

            if (categoryType == null)
            {
                return NotFound();
            }

            return View(categoryType);
        }

        // POST: CategoryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryTypeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryTypeExists(int id)
        {
            return _context.CategoryTypes.Any(e => e.ID == id);
        }
    }
}
