using Microsoft.EntityFrameworkCore;
using SnackHub.AppContext;
using SnackHub.Models;
using SnackHub.Repositories.Interfaces;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Product> GetAll()
    {
        return _context.Products.Include(p => p.Category).AsQueryable();
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == productId);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int productId)
    {
        var product = await GetProductByIdAsync(productId);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
