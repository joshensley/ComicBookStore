using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Models
{
    public class ProfileImage
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Application User ID")]
        public string ApplicationUserID { get; set; }

        [Required]
        [Display(Name = "File Name")]
        public string ImageFileName { get; set; }

        [Required]
        [Display(Name = "Is Profile Image")]
        public bool IsProfileImage { get; set; }

        [Required]
        [Display(Name = "Created At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd)}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Display(Name = "Updated At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdateAt { get; set; }

    }
}
