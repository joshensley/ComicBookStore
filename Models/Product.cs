using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(512, ErrorMessage = "{0} can have a maximum of {1} characters")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(3000, ErrorMessage = "{0} can have a maximum of {1} characters")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Regular Price")]
        public decimal RegularPrice { get; set; }

        [Required]
        [Display(Name = "Discount Price")]
        public decimal DiscountPrice { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Created At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Updated At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int ProductTypeID { get; set; }
        public ProductType ProductType { get; set; }

        [Required]
        public int CategoryTypeID { get; set; }
        public CategoryType CategoryType { get; set; }

        public ICollection<ProductSpecificationValue> ProductSpecificationValues { get; set; }

        public ICollection<ProductInventory> ProductInventory { get; set; }

    }
}
