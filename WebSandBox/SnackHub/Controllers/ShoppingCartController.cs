using Microsoft.AspNetCore.Mvc;
using SnackHub.Models;
using SnackHub.Repositories.Interfaces;
using SnackHub.ViewModels;

namespace SnackHub.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IProductRepository productRepository, ShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
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

        [HttpPost]
        public IActionResult AddItemToShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.GetAll().FirstOrDefault(p => p.ProductId == productId);

            if (selectedProduct != null)
            {
                _shoppingCart.AddItemToCart(selectedProduct);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveItemFromShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.GetAll().FirstOrDefault(p => p.ProductId == productId);

            if (selectedProduct != null)
            {
                _shoppingCart.RemoveItemFromCart(selectedProduct);
            }

            return RedirectToAction("Index");
        }
    }
}
