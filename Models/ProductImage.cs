using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Models
{
    public class ProductImage
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "ProductID")]
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "File Name")]
        public string ImageFileName { get; set; }

        [NotMapped]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Is Feature")]
        public bool IsFeature { get; set; }

        [Required]
        [Display(Name = "Created At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Updated At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedAt { get; set; }
    }
}
