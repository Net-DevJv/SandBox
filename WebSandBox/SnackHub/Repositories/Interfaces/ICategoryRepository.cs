using SnackHub.Models;

namespace SnackHub.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAll();

        Task<Category> GetCategoryByIdAsync(int categoryId);

        Task AddAsync(Category category);

        Task UpdateAsync(Category category);

        Task DeleteAsync(int categoryId);
    }
}
