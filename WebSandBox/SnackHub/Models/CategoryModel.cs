using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnackHub.Models
{
    [Table("Categories")]
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "O nome da categoria deve ser informado")]
        [Display(Name = "Nome da Categoria")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {1} e máximo {2}")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "A descrição do categoria deve ser informada")]
        [Display(Name = "Descrição da Categoria")]
        [MinLength(20, ErrorMessage = "A descrição da categoria deve ter no mínimo {1} caracteres")]
        [MaxLength(500, ErrorMessage = "A descrição da categoria não pode exceder {1} caracteres")]
        public string CategoryDescription { get; set; }

        public List<SnackModel> Snack { set; get; }
    }
}
