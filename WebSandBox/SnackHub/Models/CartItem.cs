using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnackHub.Models
{
    [Table("CartItems")]
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        public int ProductQuantity { get; set; }

        [Required(ErrorMessage = "O ID do carrinho é obrigatório.")]
        [StringLength(200)]
        public string ShoppingCartId { get; set; }

        [Required(ErrorMessage = "O ID do produto é obrigatório.")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
