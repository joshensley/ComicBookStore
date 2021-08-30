using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Models
{
    public class ProductSpecificationValue
    {
        public int ID { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "{0} can have a maximum of {1} characters")]
        [Display(Name = "Name")]
        public string Value { get; set; }

        public int? ProductSpecificationID { get; set; }
        public ProductSpecification ProductSpecification { get; set; }

        public int? ProductID { get; set; }
        public Product Products { get; set; }
    }
}
