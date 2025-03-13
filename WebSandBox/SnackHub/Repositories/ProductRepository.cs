using Microsoft.EntityFrameworkCore;
using SnackHub.AppContext;
using SnackHub.Models;
using SnackHub.Repositories.Interfaces;

namespace SnackHub.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> Products
        {
            get
            {
                return _context.Products.Include(p => p.Category);
            }
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == productId);
        }
    }
}
