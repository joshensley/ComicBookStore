using ComicBookStore.Models;
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
        private readonly ProductsService _productsService;
        private readonly ProductTypeService _productTypeService;
        private readonly ProductSpecificationService _productSpecificationService;
        private readonly ProductSpecificationValueService _productSpecificationValueService;

        public ProductSpecificationsController(
            ProductsService productsService,
            ProductTypeService productTypeService, 
            ProductSpecificationService productSpecificationService,
            ProductSpecificationValueService productSpecificationValueService)
        {
            _productsService = productsService;
            _productTypeService = productTypeService;
            _productSpecificationService = productSpecificationService;
            _productSpecificationValueService = productSpecificationValueService;
        }

        public async Task<IActionResult> Index()
        {
            var productTypes = (await _productTypeService.GetProductTypeWithProductSpecificationsDTO()).Value;

            return View(productTypes);
        }

        public async Task<IActionResult> Create(int id)
        {
            var productType = (await _productTypeService.GetByIdProductTypeWithProductSpecificationsDTO(id)).Value;

            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            int id, 
            [Bind("ProductSpecification")] IList<ProductSpecification> productSpecification)
        {
            if (ModelState.IsValid)
            {
                // Creates the new productSpecifications
                var productSpecifications = (await _productSpecificationService.PostProductSpecificationRange(productSpecification)).Value;

                // Finds all products that match productTypeID
                var products = (await _productsService.GetProductDTOByProductTypeID(id)).Value;

                // Creates new productSpecificationValues for the current products in the database
                var productSpecificationValues = _productSpecificationService.CreateProductSpecificationValueList(productSpecifications, products);

                // Adds the new productSpecificationValues in the database
                var response = (await _productSpecificationValueService.PostRange(productSpecificationValues)).Value;

                if (response) return RedirectToAction(nameof(Create), new { id = id });
            }

            var productType = (await _productTypeService.GetByIdProductTypeWithProductSpecificationsDTO(id)).Value;

            return View(productType);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var productType = (await _productTypeService.GetByIdProductTypeWithProductSpecificationsDTO(id)).Value;

            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("ProductSpecification")] IList<ProductSpecification> productSpecification)
        {
            if (ModelState.IsValid)
            {
                var response = (await _productSpecificationService.EditProductSpecificationsRange(productSpecification)).Value;

                if (response != null) return RedirectToAction(nameof(Index));
            }

            var productType = (await _productTypeService.GetByIdProductTypeWithProductSpecificationsDTO(id)).Value;

            return View(productType);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var productType = (await _productTypeService.GetByIdProductTypeWithProductSpecificationsDTO(id)).Value;

            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<int>> DeleteAction(int id)
        {
            // Delete ProductSpecificationValue List
            var productSpecificationValue = (await _productSpecificationValueService.DeleteByProductSpecificationID(id)).Value;

            // Delete ProductSpecification
            var productSpecification = (await _productSpecificationService.DeleteProductSpecificationByID(id)).Value;

            if (productSpecification == true && productSpecificationValue == true)
            {
                return id;
            }

            return NotFound();
        }
    }
}
