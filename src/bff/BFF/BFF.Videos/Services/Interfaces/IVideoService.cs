using BFF.Videos.Data.Entities;

namespace BFF.Videos.Services.Interfaces
{
    public interface IVideoService
    {
        Task<IEnumerable<Video>> GetAllAsync();

        Task<int> CreateAsync(Video video, IFormFile file);
    }
}