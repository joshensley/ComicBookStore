using ComicBookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Controllers
{
    public class ProductSpecificationsController : Controller
    {
        private readonly ProductTypeService _productTypeService;

        public ProductSpecificationsController(ProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var productTypes = (await _productTypeService.GetProductTypeWithProductSpecificationsDTO()).Value;

            return View(productTypes);
        }

        public IActionResult Create(int id)
        {
            return View();
        }
    }
}
