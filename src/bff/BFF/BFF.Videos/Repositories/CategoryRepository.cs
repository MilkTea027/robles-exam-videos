using BFF.Videos.Data;
using BFF.Videos.Data.Entities;
using BFF.Videos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BFF.Videos.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
