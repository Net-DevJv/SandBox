using Microsoft.EntityFrameworkCore;
using SnackHub.AppContext;

namespace SnackHub.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _context;

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public string CartId { get; set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public static ShoppingCart GetOrCreateCart(IServiceProvider services)
        {
            var httpContextAccessor = services.GetRequiredService<IHttpContextAccessor>();
            ISession session = httpContextAccessor.HttpContext.Session;

            var context = services.GetRequiredService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context)
            {
                CartId = cartId
            };
        }

        public List<CartItem> GetCartItems()
        {
            return Items ?? (Items = _context.CartItems
                .Where(ci => ci.ShoppingCartId == this.CartId)
                .Include(ci => ci.Product)
                .ToList());
        }

        public void AddItemToCart(Product product)
        {
            var cartItem = _context.CartItems.SingleOrDefault(ci => ci.ProductId == product.ProductId && ci.ShoppingCartId == this.CartId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ShoppingCartId = this.CartId,
                    ProductId = product.ProductId,
                    ProductQuantity = 1,
                    Product = product
                };

                _context.CartItems.Add(cartItem);
                Items.Add(cartItem);
            }
            else
            {
                cartItem.ProductQuantity++;
            }

            _context.SaveChanges();
        }

        public int RemoveItemFromCart(Product product)
        {
            var cartItem = _context.CartItems.SingleOrDefault(ci => ci.ProductId == product.ProductId && ci.ShoppingCartId == this.CartId);

            int remainingQuantity = 0;

            if (cartItem != null)
            {
                if (cartItem.ProductQuantity > 1)
                {
                    cartItem.ProductQuantity--;
                    remainingQuantity = cartItem.ProductQuantity;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
            }

            _context.SaveChanges();

            return remainingQuantity;
        }

        public void ClearCart()
        {
            var cartItems = _context.CartItems.Where(ci => ci.ShoppingCartId == this.CartId);

            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public decimal GetShoppingCartAmount()
        {
            var totalAmount = _context.CartItems
                .Where(ci => ci.ShoppingCartId == this.CartId)
                .Select(ci => ci.Product.Price * ci.ProductQuantity)
                .Sum();

            return totalAmount;
        }
    }
}
