using ComicBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ComicBookStore.Repositories.DTO
{
    public class CartItemDTO
    {
        public int ID { get; set; }
        public string ApplicationUserID { get; set; }
        public int Quantity { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public static Expression<Func<Cart, CartItemDTO>> CartItemSelector
        {
            get
            {
                return cartItem => new CartItemDTO()
                {
                    ID = cartItem.ID,
                    ApplicationUserID = cartItem.ApplicationUserID,
                    Quantity = cartItem.Quantity,
                    ProductID = cartItem.ProductID,
                    Product = cartItem.Product 
                };
            }
        }
    }
}
