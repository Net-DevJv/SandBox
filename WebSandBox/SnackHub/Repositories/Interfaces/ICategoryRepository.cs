using SnackHub.Models;

namespace SnackHub.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryModel> Categories { get; }
    }
}
