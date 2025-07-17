using BFF.Videos.Controllers;
using BFF.Videos.Data.Entities;
using BFF.Videos.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BFF.Videos.Tests.Controllers
{
    public class VideosControllerTest
    {
        private readonly Mock<IVideoService> _mockVideoService;
        private readonly VideosController _controller;

        public VideosControllerTest()
        {
            _mockVideoService = new Mock<IVideoService>();
            _controller = new VideosController(_mockVideoService.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithListOfVideos()
        {
            // Arrange
            var videos = new List<Video> { new Video { Id = 1, Name = "Test Video" } };
            _mockVideoService.Setup(service => service.GetAllAsync()).ReturnsAsync(videos);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Video>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WhenValid()
        {
            // Arrange
            var video = new Video { Name = "New Video", CategoryId = 1 };
            var fileMock = new Mock<IFormFile>();
            var thumbnailMock = new Mock<IFormFile>();

            _mockVideoService
                .Setup(service => service.CreateAsync(video, fileMock.Object, thumbnailMock.Object))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.Create(video, fileMock.Object, thumbnailMock.Object);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(1, createdResult.Value);
        }
    }
}