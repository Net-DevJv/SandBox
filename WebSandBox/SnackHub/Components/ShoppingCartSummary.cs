using SnackHub.Models;
using SnackHub.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace SnackHub.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var cartItems = _shoppingCart.GetCartItems();
            _shoppingCart.Items = cartItems;

            var cartItemViewModels = cartItems.Select(ci => CartItemViewModel.CreateItem(ci)).ToList();

            var shoppingCartVM = new ShoppingCartViewModel
            {
                Items = cartItemViewModels,
                Subtotal = _shoppingCart.GetShoppingCartAmount()
            };

            return View(shoppingCartVM);
        }
    }
}
