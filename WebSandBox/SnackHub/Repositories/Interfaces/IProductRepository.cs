using SnackHub.Models;

namespace SnackHub.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAll();

        Task<Product> GetProductByIdAsync(int productId);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(int productId);
    }
}
