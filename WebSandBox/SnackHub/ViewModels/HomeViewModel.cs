using SnackHub.Models;

namespace SnackHub.ViewModels
{
    public class HomeViewModel
    {

        public IEnumerable<SnackModel> FavoriteSnacks { get; set; }
    }
}
