using ComicBookStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories.DTO
{
    public class ProductSpecificationDTO
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Product Type")]
        public int ProductTypeID { get; set; }

        public static Expression<Func<ProductSpecification, ProductSpecificationDTO>> ProductSpecificationSelector
        {
            get
            {
                return productSpecification => new ProductSpecificationDTO()
                {
                    ID = productSpecification.ID,
                    Name = productSpecification.Name,
                    ProductTypeID = productSpecification.ProductTypeID
                };
            }
        }
    }
}
