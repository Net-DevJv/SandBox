using SnackHub.Models;

namespace SnackHub.Repositories.Interfaces
{
    public interface ISnacksRepository
    {
        IEnumerable<SnackModel> Snacks { get; }

        IEnumerable<SnackModel> FavoriteSnacks { get; }

        SnackModel GetSnackById(int snackId);
    }
}
