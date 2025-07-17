using BFF.Videos.Data;
using BFF.Videos.Data.Entities;
using BFF.Videos.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BFF.Videos.Tests.Repositories
{
    public class CategoryRepositoryTest
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"CategoryDb_{Guid.NewGuid()}")
                .Options;

            var context = new AppDbContext(options);
            context.Categories.AddRange(new List<Category>
            {
                new Category { Id = 1, Name = "Music" },
                new Category { Id = 2, Name = "Education" }
            });

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllCategories()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new CategoryRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            var categories = result.ToList();
            Assert.Equal(2, categories.Count);
            Assert.Contains(categories, c => c.Name == "Music");
            Assert.Contains(categories, c => c.Name == "Education");
        }
    }
}
