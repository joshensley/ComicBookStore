using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Models.ViewModels
{
    public class ProfileImageViewModel
    {
        public int ProfileImageID { get; set; }
        public string ApplicationUserID { get; set; }
        public string ImageFileName { get; set; }
        public bool IsProfileImage { get; set; }
        public string URL { get; set; }
    }
}
