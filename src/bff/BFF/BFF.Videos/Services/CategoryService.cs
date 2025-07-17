using BFF.Videos.Data.Entities;
using BFF.Videos.Repositories.Interfaces;
using BFF.Videos.Services.Interfaces;

namespace BFF.Videos.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}