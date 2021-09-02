using ComicBookStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories.DTO
{
    public class InventoryItemDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Is Active is required")]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "In Stock is required")]
        [Display(Name = "In Stock")]
        public bool InStock { get; set; }

        [Required(ErrorMessage = "Created At is required")]
        [Display(Name = "Created At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Updated At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Product is required")]
        public int ProductID { get; set; }

        public static Expression<Func<ProductInventory, InventoryItemDTO>> ProductInventorySelector
        {
            get
            {
                return productInventory => new InventoryItemDTO()
                {
                    ID = productInventory.ID,
                    InStock = productInventory.InStock,
                    IsActive = productInventory.IsActive,
                    CreatedAt = productInventory.CreatedAt,
                    UpdatedAt = productInventory.UpdatedAt,
                    ProductID = productInventory.ProductID
                };
            }
        }
    }
}
