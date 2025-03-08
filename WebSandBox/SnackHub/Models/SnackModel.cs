using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnackHub.Models
{
    [Table("Snacks")]
    public class SnackModel
    {
        [Key]
        public int SnackId { get; set; }

        [Required(ErrorMessage = "O nome do produto deve ser informado")]
        [Display(Name = "Nome da Produto")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {1} e máximo {2}")]
        public string SnackName { get; set; }

        [Required(ErrorMessage = "O resumo do produto deve ser informado")]
        [Display(Name = "Resumo do Produto")]
        [MinLength(20, ErrorMessage = "O resumo do produto deve ter no mínimo {1} caracteres")]
        [MaxLength(500, ErrorMessage = "O resumo do produto não pode exceder {1} caracteres")]
        public string ProductSummary { get; set; }

        [Required(ErrorMessage = "A descrição do produto deve ser informada")]
        [Display(Name = "Descrição do Produto")]
        [MinLength(20, ErrorMessage = "A descrição do produto deve ter no mínimo {1} caracteres")]
        [MaxLength(1000, ErrorMessage = "A descrição do produto não pode exceder {1} caracteres")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "O preço do produto deve ser informado")]
        [Display(Name = "Preço do Produto")]
        [Column(TypeName = "decimal(10, 2)")]
        [Range(1,999.99, ErrorMessage = "O preço deve estar entre 1 e 999,99")]
        public decimal Price { get; set; }

        [Display(Name = "URL da Imagem")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1}")]
        public string ImageUrl { get; set; }

        [Display(Name = "URL da Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1}")]
        public string ImageThumbnailUrl { get; set; }

        [Display(Name = "Lanche preferido?")]
        public bool IsPreferredSnack { get; set; }

        [Display(Name = "Em estoque?")]
        public bool InStock { get; set; }

        public int CategoryId { get; set; }

        public virtual CategoryModel Category { get; set; }
    }
}
