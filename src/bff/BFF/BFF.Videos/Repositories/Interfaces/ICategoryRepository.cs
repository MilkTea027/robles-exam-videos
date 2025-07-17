using BFF.Videos.Data.Entities;

namespace BFF.Videos.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}