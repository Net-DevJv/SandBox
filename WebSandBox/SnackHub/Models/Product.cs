using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
        [MaxLength(1000, ErrorMessage = "* O resumo do produto não pode exceder {1} caracteres")]
        public string? Summary { get; set; }

        [Display(Name = "Descrição do produto")]
        [MinLength(20, ErrorMessage = "* A descrição do produto deve ter no mínimo {1} caracteres")]
        [MaxLength(1000, ErrorMessage = "* A descrição do produto não pode exceder {1} caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "* O preço do produto deve ser informado!")]
        [Display(Name = "Preço do produto")]
        [Column(TypeName = "decimal(10, 2)")]
        [Range(1, 999.99, ErrorMessage = "* O preço deve estar entre 1 e 999,99")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public decimal Price { get; set; }

        [ValidateNever]
        [Display(Name = "Imagem do Produto")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Produto disponível no estoque?")]
        public bool InStock { get; set; }

        [Display(Name = "Selecionar categoria")]
        public int? CategoryId { get; set; }

        [ValidateNever]
        public virtual Category? Category { get; set; }
    }
}
