using BFF.Videos.Controllers;
using BFF.Videos.Data.Entities;
using BFF.Videos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BFF.Videos.Tests.Controllers
{
    public class CategoriesControllerTest
    {
        [Fact]
        public async Task Get_ReturnsOkWithCategories()
        {
            // Arrange
            var mockService = new Mock<ICategoryService>();
            var sampleCategories = new List<Category>
            {
                new Category { Id = 1, Name = "Education" },
                new Category { Id = 2, Name = "Entertainment" }
            };

            mockService.Setup(s => s.GetAllAsync())
                       .ReturnsAsync(sampleCategories);

            var controller = new CategoriesController(mockService.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Category>>(okResult.Value);
            Assert.Equal(2, ((List<Category>)returnValue).Count);
        }

        [Fact]
        public async Task Get_ReturnsEmptyList_WhenNoCategories()
        {
            // Arrange
            var mockService = new Mock<ICategoryService>();
            mockService.Setup(s => s.GetAllAsync())
                       .ReturnsAsync(new List<Category>());

            var controller = new CategoriesController(mockService.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var categories = Assert.IsType<List<Category>>(okResult.Value);
            Assert.Empty(categories);
        }
    }
}