using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SnackHub.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "* O nome do produto deve ser informado!")]
        [Display(Name = "Nome do produto")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "* O {0} deve ter no mínimo {1} e máximo {2}")]
        public string Name { get; set; }

        [Display(Name = "Resumo do produto")]
        [MinLength(20, ErrorMessage = "* O resumo do produto deve ter no mínimo {1} caracteres")]
        [MaxLength(500, ErrorMessage = "* O resumo do produto não pode exceder {1} caracteres")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "* A descrição do produto deve ser informada!")]
        [Display(Name = "Descrição do produto")]
        [MinLength(20, ErrorMessage = "* A descrição do produto deve ter no mínimo {1} caracteres")]
        [MaxLength(1000, ErrorMessage = "* A descrição do produto não pode exceder {1} caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "* O preço do produto deve ser informado!")]
        [Display(Name = "Preço do produto")]
        [Column(TypeName = "decimal(10, 2)")]
        [Range(1, 999.99, ErrorMessage = "* O preço deve estar entre 1 e 999,99")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "* A imagem do produto deve ser inserida!")]
        [Display(Name = "Imagem do Produto")]
        [StringLength(200, ErrorMessage = "* O {0} deve ter no máximo {1}")]
        public string ImageUrl { get; set; }

        [Display(Name = "Produto disponível no estoque?")]
        public bool InStock { get; set; }

        [Required(ErrorMessage = "* A categoria do produto deve ser selecionada!")]
        [Display(Name = "Selecionar categoria")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
