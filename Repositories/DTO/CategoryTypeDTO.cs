using ComicBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories.DTO
{
    public class CategoryTypeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public static Expression<Func<CategoryType, CategoryTypeDTO>> CategoryTypeSelector
        {
            get
            {
                return categoryType => new CategoryTypeDTO()
                {
                    ID = categoryType.ID,
                    Name = categoryType.Name,
                    IsActive = categoryType.IsActive
                };
            }
        }
    }
}
