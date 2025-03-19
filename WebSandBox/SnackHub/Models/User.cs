using SnackHub.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnackHub.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [EmailAddress]
        [Display(Name = "e-mail")]
        [Required(ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL010")]
        [StringLength(256, MinimumLength = 10, ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL002")]
        public string Email { get; set; }

        [Display(Name = "nome e sobrenome")]
        [Required(ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL010")]
        [StringLength(150, MinimumLength = 10, ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL002")]
        public string FullName { get; set; }

        [Display(Name = "senha")]
        [Required(ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL009")]
        public string PasswordHash { get; set; }

        [Required]
        public UserType UserType { get; set; }

        [Display(Name = "Data de criação")]
        [DataType(DataType.Date)]
        public DateTime? CreationDate { get; set; }

        [Display(Name = "Data de atualização")]
        [DataType(DataType.Date)]
        public DateTime? UpdateDate { get; set; }
    }
}
