using Microsoft.AspNetCore.Mvc;
using SnackHub.Models;
using SnackHub.Repositories.Interfaces;

namespace SnackHub.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCartModel _shoppingCartModel;

        public OrderController(IOrderRepository orderRepository, ShoppingCartModel shoppingCartModel)
        {
            _orderRepository = orderRepository;
            _shoppingCartModel = shoppingCartModel;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(OrderModel order)
        {
            int totalItemsOrdered = 0;
            decimal priceTotalOrder = 0.0m;

            List<CartItemModel> items = _shoppingCartModel.GetCartItems();
            _shoppingCartModel.CartItems = items;

            if(_shoppingCartModel.CartItems.Count == 0)
            {
                ModelState.AddModelError("", "Carrinho vazio, necessário adicionar itens...");
            }
            foreach(var item in items)
            {
                totalItemsOrdered += item.Amount;
                priceTotalOrder += (item.Snack.Price * item.Amount);
            }

            order.TotalItemsOrdered = totalItemsOrdered;
            order.OrderTotal = priceTotalOrder;

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);

                ViewBag.CheckoutCompleteMessage = "Pedido feito";
                ViewBag.OrderTotal = _shoppingCartModel.GetShoppingCartAmount();

                _shoppingCartModel.ClearCart();

                return View("~/Views/Order/CheckoutComplete.cshtml", order);
            }

            return View(order);
        }
    }
}
