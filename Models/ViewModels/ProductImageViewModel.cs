using ComicBookStore.Repositories.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Models.ViewModels
{
    public class ProductImageViewModel
    {
        public ProductDTO ProductDTO { get; set; }
        public int ProductID { get; set; }
        public List<string> ProductImagesURL { get; set; }
        public IFormFile FileUpload { get; set; }
    }
}
