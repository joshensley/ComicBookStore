using ComicBookStore.Data;
using ComicBookStore.Models;
using ComicBookStore.Models.ViewModels;
using ComicBookStore.Repositories.DTO;
using ComicBookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CategoryTypeService _categoryTypeService;
        private readonly ProductTypeService _productTypeService;
        private readonly ProductSpecificationService _productSpecificationService;
        private readonly ProductSpecificationValueService _productSpecificationValueService;
        private readonly ProductsService _productsService;

        public ProductsController(
            ApplicationDbContext db,
            CategoryTypeService categoryTypeService,
            ProductTypeService productTypeService,
            ProductSpecificationService productSpecificationService,
            ProductSpecificationValueService productSpecificationValueService,
            ProductsService productsService)
        {
            _db = db;
            _categoryTypeService = categoryTypeService;
            _productTypeService = productTypeService;
            _productSpecificationService = productSpecificationService;
            _productSpecificationValueService = productSpecificationValueService;
            _productsService = productsService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var productsDetail = (await _productsService.GetProductDetailDTO()).Value;
            return View(productsDetail);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var productDetail = (await _productsService.GetByIdProductDetailDTO(id)).Value;

            if (productDetail == null)
            {
                return NotFound();
            }

            return View(productDetail);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            // Change to get all ACTIVE product types and category types
            var productTypes = (await _productTypeService.GetProductTypeDTO()).Value;
            var categoryTypes = (await _categoryTypeService.GetCategoryTypeDTO()).Value;

            var createProductViewModel = new CreateProductViewModel()
            {
                ProductTypes = productTypes,
                CategoryTypes = categoryTypes
            };

            return View(createProductViewModel);
        }

        // GET: Products/GetProductSpecificationsByProductTypeId
        public async Task<ActionResult<IEnumerable<ProductSpecificationDTO>>> GetProductSpecificationsByProductTypeId(int id)
        {
            var productSpecifications = await _productSpecificationService.GetProductSpecificationDTOByProductTypeID(id);

            return productSpecifications;
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,Description,RegularPrice," +
            "DiscountPrice,IsActive,ProductTypeID,CategoryTypeID," +
            "CreatedAt,UpdatedAt,ProductSpecificationNameValue")] CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Post product
                var product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    RegularPrice = model.RegularPrice,
                    DiscountPrice = model.DiscountPrice,
                    IsActive = model.IsActive,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    ProductTypeID = model.ProductTypeID,
                    CategoryTypeID = model.CategoryTypeID
                };

                await _productsService.Post(product);

                if (model.ProductSpecificationNameValue != null)
                {
                    // Post product specification values
                    var productId = product.ID;
                    foreach (var item in model.ProductSpecificationNameValue)
                    {
                        var productSpecificationValue = new ProductSpecificationValue()
                        {
                            Value = item.Value == null ? "" : item.Value,
                            ProductSpecificationID = item.ProductSpecificationID,
                            ProductID = productId
                        };

                        await _productSpecificationValueService.Post(productSpecificationValue);
                    }
                }
               
                
                return RedirectToAction(nameof(Index));
            }

            var productTypes = (await _productTypeService.GetProductTypeDTO()).Value;
            var categoryTypes = (await _categoryTypeService.GetCategoryTypeDTO()).Value;
            model.CategoryTypes = categoryTypes;
            model.ProductTypes = productTypes;

            return View(model);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var productDetail = (await _productsService.GetByIdProductDetailDTO(id)).Value;

            var productSpecificationNameValues = new List<ProductSpecificationNameValue>();
            foreach (var item in productDetail.ProductSpecficationNameValue)
            {
                var productSpecficationNameValue = new ProductSpecificationNameValue()
                {
                    Name = item.Name,
                    Value = item.Value,
                    ProductSpecificationID = item.ProductSpecificationID,
                    ProductSpecificationValueID = item.ProductSpecificationValueID
                };

                productSpecificationNameValues.Add(productSpecficationNameValue);
            }

            var updateProductViewModel = new CreateProductViewModel()
            {
                ID = productDetail.ID,
                Name = productDetail.Name,
                Description = productDetail.Description,
                RegularPrice = productDetail.RegularPrice,
                DiscountPrice = productDetail.DiscountPrice,
                IsActive = productDetail.IsActive,
                CreatedAt = productDetail.CreatedAt,
                UpdatedAt = productDetail.UpdatedAt,
                ProductTypeID = productDetail.ProductTypeID,
                CategoryTypeID = productDetail.CategoryTypeID,
                ProductSpecificationNameValue = productSpecificationNameValues
            };

            return View(updateProductViewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("ID,Name,Description,RegularPrice,DiscountPrice,IsActive," +
                "ProductTypeID,CategoryTypeID,CreatedAt,UpdatedAt," +
                "ProductSpecificationNameValue")] CreateProductViewModel model)
        {
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Edit product 
                    var product = new Product()
                    {
                        ID = model.ID,
                        Name = model.Name,
                        Description = model.Description,
                        RegularPrice = model.RegularPrice,
                        DiscountPrice = model.DiscountPrice,
                        IsActive = model.IsActive,
                        CreatedAt = model.CreatedAt,
                        UpdatedAt = DateTime.Now,
                        ProductTypeID = model.ProductTypeID,
                        CategoryTypeID = model.CategoryTypeID
                    };

                    await _productsService.Edit(product);

                    // Edit the product specification values
                    foreach (var item in model.ProductSpecificationNameValue)
                    {
                        var productSpecificationValue = new ProductSpecificationValue()
                        {
                            ID = item.ProductSpecificationValueID,
                            Value = item.Value,
                            ProductSpecificationID = item.ProductSpecificationID,
                            ProductID = model.ID
                        };

                        await _productSpecificationValueService.Edit(productSpecificationValue);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(model.ID))
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

            return View(model);
        }

        // DELETE: Products/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        private bool ProductExists(int id)
        {
            return _db.Products.Any(p => p.ID == id);
        }
    }
}
