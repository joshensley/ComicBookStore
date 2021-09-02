using ComicBookStore.Repositories.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Models.ViewModels
{
    public class CreateProductViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Regular Price is required")]
        [Display(Name = "Regular Price")]
        public decimal RegularPrice { get; set; }

        [Required(ErrorMessage = "Discount Price is required")]
        [Display(Name = "Discount Price")]
        public decimal DiscountPrice { get; set; }

        [Required(ErrorMessage = "Active is required")]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Updated")]
        public DateTime UpdatedAt { get; set; }

        [Required(ErrorMessage = "Product Type is required")]
        [Display(Name = "Product Type ID")]
        public int ProductTypeID { get; set; }

        [Required(ErrorMessage = "Category Type is required")]
        [Display(Name = "Category Type ID")]
        public int CategoryTypeID { get; set; }

        public IList<ProductSpecificationNameValue> ProductSpecificationNameValue { get; set; }

        public IEnumerable<CategoryTypeDTO> CategoryTypes { get; set; }

        public IEnumerable<ProductTypeDTO> ProductTypes { get; set; }

    }
}
