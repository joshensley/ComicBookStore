using ComicBookStore.Models;
using ComicBookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories.DTO
{
    public class ProductDTO
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Regular Price")]
        public decimal RegularPrice { get; set; }

        [Display(Name = "Discount Price")]
        public decimal DiscountPrice { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Updated")]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Product Type ID")]
        public int ProductTypeID { get; set; }

        [Display(Name = "Product")]
        public string ProductTypeName { get; set; }

        [Display(Name = "Category Type ID")]
        public int CategoryTypeID { get; set; }

        public IEnumerable<ProductSpecificationNameValue> ProductSpecficationNameValue { get; set; }

        public IEnumerable<ProductImage> ProductImages { get; set; }

        public static Expression<Func<Product, ProductDTO>> ProductSelector
        {
            get
            {
                return product => new ProductDTO()
                {
                    ID = product.ID,
                    Name = product.Name,
                    Description = product.Description,
                    RegularPrice = product.RegularPrice,
                    DiscountPrice = product.DiscountPrice,
                    IsActive = product.IsActive,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt,
                    ProductTypeID = product.ProductTypeID,
                    CategoryTypeID = product.CategoryTypeID
                };
            }
        }

        public static Expression<Func<Product, ProductDTO>> ProductDetailSelector
        {
            get
            {
                return product => new ProductDTO()
                {
                    ID = product.ID,
                    Name = product.Name,
                    Description = product.Description,
                    RegularPrice = product.RegularPrice,
                    DiscountPrice = product.DiscountPrice,
                    IsActive = product.IsActive,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt,
                    ProductTypeName = product.ProductType.Name,
                    ProductTypeID = product.ProductTypeID,
                    CategoryTypeID = product.CategoryTypeID,
                    ProductSpecficationNameValue = product.ProductSpecificationValues
                        .Select(p => new ProductSpecificationNameValue()
                        {
                            Name = p.ProductSpecification.Name,
                            Value = p.Value,
                            ProductSpecificationID = p.ProductSpecification.ID,
                            ProductSpecificationValueID = p.ID
                        }),
                    ProductImages = product.ProductImages
                        .Select(p => new ProductImage()
                        {
                            ID = p.ID,
                            ImageFileName = p.ImageFileName,
                            IsFeature = p.IsFeature,
                            CreatedAt = p.CreatedAt,
                            UpdatedAt = p.UpdatedAt
                        })
                };
            }
        }

    }
}
