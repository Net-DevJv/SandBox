using System.ComponentModel.DataAnnotations;

namespace SnackHub.ViewModels
{
    public class LoginEmailViewModel
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        [RegularExpression(@"^[^@\s]+@gmail\.com(\.br)?$", ErrorMessage = "Insira um e-mail válido")]
        public string Email { get; set; }
    }
}
