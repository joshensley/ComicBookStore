using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicBookStore.Models.ViewModels
{
    public class ProductSpecificationNameValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int ProductSpecificationID { get; set; }
        public int ProductSpecificationValueID { get; set; }
    }
}
