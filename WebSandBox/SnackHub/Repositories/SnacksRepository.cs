using Microsoft.EntityFrameworkCore;
using SnackHub.AppDbContext;
using SnackHub.Models;
using SnackHub.Repositories.Interfaces;

namespace SnackHub.Repositories
{
    public class SnacksRepository : ISnacksRepository
    {
        private readonly WebAppDbContext _context;

        public SnacksRepository(WebAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SnackModel> Snacks
        {
            get { return _context.Snacks.Include(c => c.Category); }
        }

        public IEnumerable<SnackModel> FavoriteSnacks
        {
            get
            {
                return _context.Snacks.Where(s => s.IsPreferredSnack).Include(c => c.Category);
            }
        }
        public SnackModel GetSnackById(int snackId)
        {
            return _context.Snacks.Include(s => s.Category).FirstOrDefault(s => s.SnackId == snackId);
        }
    }
}
