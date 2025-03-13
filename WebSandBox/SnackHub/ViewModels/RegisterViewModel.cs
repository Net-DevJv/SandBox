using System.ComponentModel.DataAnnotations;

namespace SnackHub.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Insira um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório")]
        [StringLength(150, ErrorMessage = "O nome completo deve ter no máximo 150 caracteres")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A confirmação da senha é obrigatória")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }
}
