using Microsoft.AspNetCore.Mvc;
using SnackHub.Models;
using SnackHub.ViewModels;

namespace SnackHub.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCartModel _shoppingCart;

        public ShoppingCartSummary(ShoppingCartModel shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var itens = _shoppingCart.GetCartItems();
            _shoppingCart.CartItems = itens;

            var shoppingCartVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartAmount = _shoppingCart.GetShoppingCartAmount()
            };

            return View(shoppingCartVM);
        }
    }
}
