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

        [Required(ErrorMessage = "* Insira seu endereço de e-mail")]
        [EmailAddress]
        [StringLength(256, ErrorMessage = "O email deve ter no máximo 256 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Insira seu nome e sobrenome")]
        [StringLength(150, ErrorMessage = "O nome completo deve ter no máximo 150 caracteres.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "* Insira sua senha")]
        public string PasswordHash { get; set; }

        [Required]
        public UserType UserType { get; set; }
    }
}
