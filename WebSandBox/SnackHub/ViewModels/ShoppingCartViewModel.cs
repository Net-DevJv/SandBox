using SnackHub.Models;

namespace SnackHub.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

        public decimal Subtotal { get; set; }
    }
}
