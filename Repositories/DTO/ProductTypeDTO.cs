using ComicBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories.DTO
{
    public class ProductTypeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public static Expression<Func<ProductType, ProductTypeDTO>> ProductTypeSelector
        {
            get
            {
                return productType => new ProductTypeDTO()
                {
                    ID = productType.ID,
                    Name = productType.Name,
                    IsActive = productType.IsActive
                };
            }
        }
    }
}
