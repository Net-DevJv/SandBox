using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnackHub.Models
{
    [Table("OrderDetails")]

    public class OrderDetailsModel
    {
        [Key]
        public int OrderDetailsId { get; set; }

        public int OrderId { get; set; }

        public int SnackId { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public virtual SnackModel SnackModel { get; set; }

        public virtual OrderModel OrderModel { get; set; }
    }
}
