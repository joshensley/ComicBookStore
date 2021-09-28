using ComicBookStore.Models;
using ComicBookStore.Repositories.DTO;
using ComicBookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductTypeService _productTypeService;
        private readonly CartService _cartService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ProductTypeService productTypeService, 
            CartService cartService, 
            ILogger<HomeController> logger)
        {
            _productTypeService = productTypeService;
            _cartService = cartService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult<IEnumerable<ProductTypeDTO>>> GetProductTypes()
        {
            var productTypes = await _productTypeService.GetProductTypeDTO();

            return productTypes;
        }

        public async Task<ActionResult<List<CartItemDTO>>> GetCartItems(string userId)
        {
            return await _cartService.GetCartItemsDTO(userId);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
