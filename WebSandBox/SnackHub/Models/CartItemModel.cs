using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnackHub.Models
{
    [Table("CartItems")]

    public class CartItemModel
    {
        [Key]
        public int CartItemId { get; set; }

        public SnackModel Snack { get; set; }

        public int Amount { get; set; }

        [StringLength(200)]
        public string ShoppingCartId { get; set; }
    }
}
