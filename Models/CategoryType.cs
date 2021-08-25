using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Models
{
    public class CategoryType
    {
        public int ID { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "{0} can have a maximum of {1} characters")]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}
