using Microsoft.EntityFrameworkCore;
using SnackHub.Models;

namespace SnackHub.AppDbContext
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
        {
        }

        public DbSet<CategoryModel> Categories { get; set; }

        public DbSet<SnackModel> Snacks { get; set; }

        public DbSet<CartItemModel> CartItems { get; set; }

        public DbSet<OrderModel> Orders { get; set; }

        public DbSet<OrderDetailsModel> OrderDetails { get; set; }
    }
}
