using ComicBookStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories.DTO
{
    public class ProductInventoryDTO
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string ProductName { get; set; }

        [Display(Name = "Active")]
        public bool ProductIsActive { get; set; }

        [Display(Name = "Regular Price")]
        public decimal ProductRegularPrice { get; set; }

        [Display(Name = "Discount Price")]
        public decimal ProductDiscountPrice { get; set; }

        [Display(Name = "Type")]
        public string ProductTypeName { get; set; }

        [Display(Name = "Category")]
        public string ProductCategoryName { get; set; }

        [Display(Name = "Inventory")]
        public IEnumerable<ProductInventory> ProductInventory { get; set; }

        public static Expression<Func<Product, ProductInventoryDTO>> ProductInventorySelector
        {
            get
            {
                return productInventory => new ProductInventoryDTO()
                {
                    ID = productInventory.ID,
                    ProductName = productInventory.Name,
                    ProductIsActive = productInventory.IsActive,
                    ProductRegularPrice = productInventory.RegularPrice,
                    ProductDiscountPrice = productInventory.DiscountPrice,
                    ProductTypeName = productInventory.ProductType.Name,
                    ProductCategoryName = productInventory.CategoryType.Name,
                    ProductInventory = productInventory.ProductInventory
                        .Select(p => new ProductInventory()
                        {
                            ID = p.ID,
                            IsActive = p.IsActive,
                            InStock = p.InStock,
                            CreatedAt = p.CreatedAt,
                            UpdatedAt = p.UpdatedAt,
                            ProductID = p.ProductID
                        })
                };
            }
        }
    }
}
