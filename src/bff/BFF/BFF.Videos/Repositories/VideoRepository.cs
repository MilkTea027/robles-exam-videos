using BFF.Videos.Data;
using BFF.Videos.Data.Entities;
using BFF.Videos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BFF.Videos.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly AppDbContext _context;

        public VideoRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Video>> GetAllAsync()
        {
            return await _context.Videos.Include(v => v.Category).ToListAsync();
        }
        public async Task<int> CreateAsync(Video video)
        {
            _context.Videos.Add(video);
            await _context.SaveChangesAsync();
            return video.Id;
        }
    }
}