using System.ComponentModel.DataAnnotations;

namespace SnackHub.ViewModels
{
    public class LoginEmailViewModel
    {
        [Display(Name = "e-mail")]
        [Required(ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL001")]
        [RegularExpression(@"^[^@\s]+@gmail\.com(\.br)?$", ErrorMessageResourceType = typeof(SnackHub.Resources.ValidationMessages), ErrorMessageResourceName = "VAL006")]
        public string Email { get; set; }
    }
}
