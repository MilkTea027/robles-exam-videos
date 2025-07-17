using BFF.Videos.Data.Entities;
using BFF.Videos.Repositories.Interfaces;
using BFF.Videos.Services;
using Moq;

namespace BFF.Videos.Tests.Services
{
    public class CategoryServiceTest
    {
        [Fact]
        public async Task GetAllAsync_ReturnsCategories()
        {
            // Arrange
            var mockRepo = new Mock<ICategoryRepository>();
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Test Category 1" },
                new Category { Id = 2, Name = "Test Category 2" }
            };

            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories);
            var service = new CategoryService(mockRepo.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);

            var categoryList = Assert.IsAssignableFrom<IEnumerable<Category>>(result);
            Assert.Collection(categoryList,
                item => Assert.Equal("Test Category 1", item.Name),
                item => Assert.Equal("Test Category 2", item.Name));
        }
    }
}
