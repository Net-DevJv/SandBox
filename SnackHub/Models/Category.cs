using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SnackHub.Enums;

namespace SnackHub.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "nome da categoria")]
        [Required(ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL011")]
        [StringLength(50, MinimumLength = 5, ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL002")]
        public string Name { get; set; }

        [Display(Name = "descrição da categoria")]
        [Required(ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL003")]
        [StringLength(500, MinimumLength = 20, ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL008")]
        public string Description { get; set; }

        [Display(Name = "Status da Categoria")]
        public CategoryStatus Status { get; set; } = CategoryStatus.Visible;

        [DataType(DataType.Date)]
        public DateTime? CreationDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? UpdateDate { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
