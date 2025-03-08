using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnackHub.Models
{
    [Table("Orders")]

    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Nome")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe o sobrenome")]
        [Display(Name = "Sobrenome")]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Informe o endereço da entrega")]
        [Display(Name = "Endereço")]
        [StringLength(100)]
        public string Address { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(100)]
        public string? AddressComplement { get; set; }

        [Required(ErrorMessage = "Informe o CEP da entrega")]
        [Display(Name = "CEP")]
        [StringLength(10, MinimumLength = 8)]
        public string CEP { get; set; }

        [Display(Name = "Estado")]
        [StringLength(50)]
        public string State { get; set; }

        [Display(Name = "Cidade")]
        [StringLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "Informe o telefone de contato")]
        [Display(Name = "Telefone")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Informe o email de contato")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "O email não possui o formato correto")]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Total do pedido")]
        public decimal OrderTotal { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Total de itens no pedido")]
        public int TotalItemsOrdered { get; set; }

        [Display(Name = "Data do pedido")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime RequestSent { get; set; }

        [Display(Name = "Data de envio do pedido")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? OrderDeliveredIn { get; set; }

        public List<OrderDetailsModel> OrderItems { get; set; }
    }
}
