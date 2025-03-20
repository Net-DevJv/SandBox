using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SnackHub.AppContext;
using SnackHub.Models;
using SnackHub.Repositories.Interfaces;
using X.PagedList.Extensions;

namespace SnackHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;


        public AdminProductsController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment,  AppDbContext context)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? categoryId, bool? inStock, DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10)
        {
            ViewBag.SearchString = searchString;
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.InStockFilter = inStock;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            var allCategories = await _context.Categories.ToListAsync();
            ViewBag.Categories = new SelectList(allCategories, "CategoryId", "Name", categoryId);

            var products = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }
            if (inStock.HasValue)
            {
                products = products.Where(p => p.InStock == inStock.Value);
            }
            if (startDate.HasValue)
            {
                products = products.Where(p => p.CreationDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                products = products.Where(p => p.CreationDate <= endDate.Value);
            }

            products = products.OrderBy(p => p.Name);
            var pagedList = await Task.FromResult(products.ToPagedList(page, pageSize));

            return View(pagedList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile ImageFile)
        {
            ModelState.Remove("ImageFile");

            if (!ModelState.IsValid)
            {
                var categories = await _context.Categories.ToListAsync();
                ViewBag.Categories = new SelectList(categories, "CategoryId", "Name", product.CategoryId);
                return View(product);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var folder = Path.Combine("wwwroot", "img", "products");
                Directory.CreateDirectory(folder);

                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                product.ImageUrl = $"/img/products/{fileName}";
            }

            product.CreationDate = DateTime.Now;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var product = await _context.Products.FindAsync(id.Value);
            if (product == null)
                return NotFound();

            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile? newImage, bool removeImage)
        {
            if (id != product.ProductId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                var dbProd = await _context.Products.FindAsync(id);
                if (dbProd != null)
                    product.ImageUrl = dbProd.ImageUrl;

                var categories = await _context.Categories.ToListAsync();
                ViewBag.Categories = new SelectList(categories, "CategoryId", "Name", product.CategoryId);

                return View(product);
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Summary = product.Summary;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.InStock = product.InStock;
            existingProduct.CategoryId = product.CategoryId;

            if (removeImage)
            {
                existingProduct.ImageUrl = null;
            }

            if (newImage != null && newImage.Length > 0)
            {
                var folder = Path.Combine(_webHostEnvironment.WebRootPath, "img", "products");
                Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(newImage.FileName);
                var filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await newImage.CopyToAsync(stream);
                }

                existingProduct.ImageUrl = $"/img/products/{fileName}";
            }

            existingProduct.UpdateDate = DateTime.Now;

            try
            {
                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(p => p.ProductId == id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.ProductId == id.Value);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.ProductId == id.Value);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int productId)
        {
            return _context.Products.Any(e => e.ProductId == productId);
        }
    }
}
