using System.ComponentModel.DataAnnotations;

namespace SnackHub.Enums
{
    public enum CategoryStatus
    {
        [Display(Name = "Oculta")]
        Hidden = 0,
        [Display(Name = "Visível")]
        Visible = 1
    }
}
