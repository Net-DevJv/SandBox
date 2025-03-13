using System.ComponentModel.DataAnnotations;

namespace SnackHub.ViewModels
{
    public class LoginPasswordViewModel
    {
        // O e-mail pode vir da etapa anterior e ser exibido apenas para referência,
        // ou ser enviado como um campo hidden
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Insira um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
