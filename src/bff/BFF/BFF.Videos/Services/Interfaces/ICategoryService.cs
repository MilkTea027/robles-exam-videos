using BFF.Videos.Data.Entities;

namespace BFF.Videos.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}