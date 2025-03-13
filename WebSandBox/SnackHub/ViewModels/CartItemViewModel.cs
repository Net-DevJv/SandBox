using SnackHub.Models;
using System;

namespace SnackHub.ViewModels
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public bool InStock { get; set; }

        public int Quantity { get; set; }

        public static CartItemViewModel CreateItem(CartItem cartItem)
        {
            return new CartItemViewModel
            {
                ProductId = cartItem.Product.ProductId,
                Name = cartItem.Product.Name,
                Price = cartItem.Product.Price,
                ImageUrl = cartItem.Product.ImageUrl,
                InStock = cartItem.Product.InStock,
                Quantity = cartItem.ProductQuantity,
            };
        }
    }
}
