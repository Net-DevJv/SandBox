using Humanizer;
using Microsoft.EntityFrameworkCore;
using SnackHub.AppDbContext;

namespace SnackHub.Models
{
    public class ShoppingCartModel
    {
        private readonly WebAppDbContext _context;

        public ShoppingCartModel(WebAppDbContext context)
        {
            _context = context;
        }

        public string ShoppingCartId { get; set; }

        public List<CartItemModel> CartItems { get; set; }

        public static ShoppingCartModel GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<WebAppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCartModel(context)
            {
                ShoppingCartId = cartId
            };
        }

        public void AddToCart(SnackModel snack)
        {
            var cartItem = _context.CartItems.SingleOrDefault(s => s.Snack.SnackId == snack.SnackId && s.ShoppingCartId == ShoppingCartId);

            if (cartItem == null)
            {
                cartItem = new CartItemModel
                {
                    ShoppingCartId = ShoppingCartId,
                    Snack = snack,
                    Amount = 1
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Amount++;
            }

            _context.SaveChanges();
        }

        public int RemoveToCart(SnackModel snack)
        {
            var cartItems = _context.CartItems.SingleOrDefault(s => s.Snack.SnackId == snack.SnackId && s.ShoppingCartId == ShoppingCartId);

            var quantityLocation = 0;

            if (cartItems != null)
            {
                if(cartItems.Amount > 1)
                {
                    cartItems.Amount--;
                    quantityLocation = cartItems.Amount;
                }
                else
                {
                    _context.CartItems.Remove(cartItems);
                }
            }

            _context.SaveChanges();

            return quantityLocation;
        }

        public List<CartItemModel> GetCartItems()
        {
            return CartItems ?? (CartItems = _context.CartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Include(s => s.Snack).ToList());
        }

        public void ClearCart()
        {
            var cartItems = _context.CartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public decimal GetShoppingCartAmount()
        {
            var quantity =  _context.CartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Select(c => c.Snack.Price * c.Amount).Sum();

            return quantity;
        }
    }
}
