using ComicBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories.DTO
{
    public class ProductTypeWithProductSpecificationsDTO
    {
        public int ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }
        public IEnumerable<ProductSpecification> ProductSpecifications { get; set; }

        public static Expression<Func<ProductType, ProductTypeWithProductSpecificationsDTO>> ProductTypeSelector
        {
            get
            {
                return productType => new ProductTypeWithProductSpecificationsDTO()
                {
                    ProductTypeID = productType.ID,
                    ProductTypeName = productType.Name,
                    ProductSpecifications = productType.ProductSpecifications
                        .Select(p => new ProductSpecification()
                        {
                            ID = p.ID,
                            Name = p.Name
                        })
                };
            }
        }
    }
}
