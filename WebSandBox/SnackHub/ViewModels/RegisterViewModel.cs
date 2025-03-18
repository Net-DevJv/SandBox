using System.ComponentModel.DataAnnotations;

namespace SnackHub.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "e-mail")]
        [Required(ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL001")]
        [RegularExpression(@"^[^@\s]+@gmail\.com(\.br)?$", ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL006")]
        public string Email { get; set; }

        [Display(Name = "nome completo")]
        [Required(ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL001")]
        [StringLength(150, MinimumLength = 10, ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL002")]
        public string FullName { get; set; }

        [Display(Name = "senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL003")]
        [StringLength(20, MinimumLength = 5, ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL008")]
        public string Password { get; set; }

        [Display(Name = "senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL004")]
        [Compare("Password", ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL005")]
        public string ConfirmPassword { get; set; }
    }
}
