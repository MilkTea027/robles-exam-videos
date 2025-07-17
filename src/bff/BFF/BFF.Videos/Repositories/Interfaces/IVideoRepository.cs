using BFF.Videos.Data.Entities;

namespace BFF.Videos.Repositories.Interfaces
{
    public interface IVideoRepository
    {
        Task<IEnumerable<Video>> GetAllAsync();

        Task<int> CreateAsync(Video video);
    }
}