using Microsoft.AspNetCore.Mvc;
using SnackHub.Models;
using SnackHub.Repositories.Interfaces;
using SnackHub.ViewModels;

namespace SnackHub.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ISnacksRepository _snacksRepository;
        private readonly ShoppingCartModel _shoppingCart;

        public ShoppingCartController(ISnacksRepository snacksRepository, ShoppingCartModel shoppingCartModel)
        {
            _snacksRepository = snacksRepository;
            _shoppingCart = shoppingCartModel;
        }

        public IActionResult Index()
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

        public IActionResult AddItemToShoppingCart(int snackId)
        {
            var selectedSnack = _snacksRepository.Snacks.FirstOrDefault(p => p.SnackId == snackId);

            if (selectedSnack != null)
            {
                _shoppingCart.AddToCart(selectedSnack);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveItemToShoppingCart(int snackId)
        {
            var selectedSnack = _snacksRepository.Snacks.FirstOrDefault(p => p.SnackId == snackId);

            if (selectedSnack != null)
            {
                _shoppingCart.RemoveToCart(selectedSnack);
            }

            return RedirectToAction("Index");
        }
    }
}
