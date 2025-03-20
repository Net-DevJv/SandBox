namespace SnackHub.Areas.Admin.ViewModels
{
    public class AdminIndexViewModel
    {
        public string AdminName { get; set; }
        public List<string> Notifications { get; set; } = new List<string>();
    }
}
