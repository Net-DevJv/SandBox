using SnackHub.AppDbContext;
using SnackHub.Models;
using SnackHub.Repositories.Interfaces;

namespace SnackHub.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebAppDbContext _webAppDbContext;
        private readonly ShoppingCartModel _shoppingCartModel;

        public OrderRepository(WebAppDbContext webAppDbContext, ShoppingCartModel shoppingCartModel)
        {
            _webAppDbContext = webAppDbContext;
            _shoppingCartModel = shoppingCartModel;
        }

        public void CreateOrder(OrderModel orderModel)
        {
            orderModel.RequestSent = DateTime.Now;
            _webAppDbContext.Orders.Add(orderModel);
            _webAppDbContext.SaveChanges();

            var shoppingCartItems = _shoppingCartModel.CartItems;

            foreach (var cartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetailsModel()
                {
                    Amount = cartItem.Amount,
                    SnackId = cartItem.Snack.SnackId,
                    OrderId = orderModel.OrderId,
                    Price = cartItem.Snack.Price
                };

                _webAppDbContext.OrderDetails.Add(orderDetail);
            }

            _webAppDbContext.SaveChanges();
        }
    }
}
