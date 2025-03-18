using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SnackHub.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "* O nome da categoria deve ser informado!")]
        [Display(Name = "Nome da Categoria")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "* O {0} deve ter no mínimo {1} e máximo {2} caracteres!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* A descrição da categoria deve ser informada!")]
        [Display(Name = "Descrição da Categoria")]
        [MinLength(20, ErrorMessage = "* A descrição da categoria deve ter no mínimo {1} caracteres!")]
        [MaxLength(500, ErrorMessage = "* A descrição da categoria não pode exceder {1} caracteres!")]
        public string Description { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
