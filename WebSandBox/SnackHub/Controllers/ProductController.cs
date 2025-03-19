using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackHub.AppContext;
using SnackHub.Enums;

namespace SnackHub.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchString, int? categoryId)
        {
            var query = _context.Products.Include(p => p.Category).Where(p => p.Category.Status == CategoryStatus.Visible);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(p => p.Name.Contains(searchString));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            query = query.OrderBy(p => p.Name);

            var result = await query.ToListAsync();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null
                || product.Category == null
                || product.Category.Status != CategoryStatus.Visible)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
