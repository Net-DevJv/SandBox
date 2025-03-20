using ApiEmployees.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEmployees.DataContext
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {
        }

        public DbSet<EmployeesModel> Employees { get; set; }
    }
}
