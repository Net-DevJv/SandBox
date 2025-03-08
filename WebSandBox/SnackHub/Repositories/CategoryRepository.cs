using SnackHub.AppDbContext;
using SnackHub.Models;
using SnackHub.Repositories.Interfaces;

namespace SnackHub.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WebAppDbContext _context;

        public CategoryRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CategoryModel> Categories
        {
            get { return _context.Categories.ToList(); }
        }
    }
}
