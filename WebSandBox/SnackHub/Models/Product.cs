using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnackHub.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "nome do produto")]
        [Required(ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL001")]
        [StringLength(200, MinimumLength = 10, ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL002")]
        public string Name { get; set; }

        [Display(Name = "resumo do produto")]
        [StringLength(1000, MinimumLength = 20, ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL002")]
        public string? Summary { get; set; }

        [Display(Name = "descrição do produto")]
        [StringLength(1000, MinimumLength = 20, ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL008")]
        public string? Description { get; set; }

        [Display(Name = "preço do produto")]
        [Column(TypeName = "decimal(10, 2)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        [Required(ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL001")]
        [Range(1, 10000.00, ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL007")]
        public decimal? Price { get; set; }

        [ValidateNever]
        [Display(Name = "Imagem do Produto")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Produto disponível no estoque?")]
        public bool InStock { get; set; }

        [Display(Name = "Selecionar categoria")]
        public int? CategoryId { get; set; }

        [ValidateNever]
        public virtual Category? Category { get; set; }

        [Display(Name = "Data de criação")]
        [DataType(DataType.Date)]
        public DateTime? CreationDate { get; set; }

        [Display(Name = "Data de atualização")]
        [DataType(DataType.Date)]
        public DateTime? UpdateDate { get; set; }
    }
}
