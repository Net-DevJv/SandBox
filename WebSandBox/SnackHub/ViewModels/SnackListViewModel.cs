using SnackHub.Models;

namespace SnackHub.ViewModels
{
    public class SnackListViewModel
    {
        public IEnumerable<SnackModel> Snacks { get; set; }

        public string CurrentCategory { get; set; }
    }
}
