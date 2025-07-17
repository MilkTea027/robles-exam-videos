using BFF.Videos.Data.Entities;
using BFF.Videos.Repositories.Interfaces;
using BFF.Videos.Services;
using Microsoft.AspNetCore.Http;
using Moq;
namespace BFF.Videos.Tests.Services
{
    public class VideoServiceTest
    {
        private readonly Mock<IVideoRepository> _mockRepo;
        private readonly VideoService _videoService;

        public VideoServiceTest()
        {
            _mockRepo = new Mock<IVideoRepository>();
            _videoService = new VideoService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnVideos()
        {
            // Arrange
            var expected = new List<Video> { new Video { Id = 1, Name = "Test" } };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expected);

            // Act
            var result = await _videoService.GetAllAsync();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task CreateAsync_ValidFiles_ShouldCallRepo()
        {
            // Arrange
            var video = new Video { Name = "Test Video" };

            var mockFile = CreateMockFormFile("video.mp4", "video/mp4");
            var mockThumb = CreateMockFormFile("thumb.jpg", "image/jpeg");

            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Video>()))
                     .ReturnsAsync(1);

            // Act
            var result = await _videoService.CreateAsync(video, mockFile.Object, mockThumb.Object);

            // Assert
            Assert.Equal(1, result);
            Assert.NotNull(video.File);
            Assert.NotNull(video.Thumbnail);
            Assert.True(video.Size > 0);
        }

        [Theory]
        [InlineData("video.exe")]
        [InlineData("video.txt")]
        public void ValidateVideoFile_InvalidExtension_ShouldThrow(string filename)
        {
            // Arrange
            var mockFile = CreateMockFormFile(filename, "application/octet-stream");

            // Act & Assert
            var ex = Assert.Throws<BadHttpRequestException>(() =>
                _videoService.ValidateVideoFile(mockFile.Object, Path.GetExtension(filename))
            );

            Assert.Equal("Invalid file type", ex.Message);
        }

        [Fact]
        public void ValidateVideoFile_TooLarge_ShouldThrow()
        {
            var file = new Mock<IFormFile>();
            file.Setup(f => f.Length).Returns(101 * 1024 * 1024); // 101MB
            file.Setup(f => f.FileName).Returns("video.mp4");

            var ex = Assert.Throws<BadHttpRequestException>(() =>
                _videoService.ValidateVideoFile(file.Object, ".mp4")
            );

            Assert.Equal("File exceeds 100MB limit", ex.Message);
        }

        private Mock<IFormFile> CreateMockFormFile(string filename, string contentType)
        {
            var content = "Dummy content";
            var bytes = System.Text.Encoding.UTF8.GetBytes(content);
            var stream = new MemoryStream(bytes);

            var file = new Mock<IFormFile>();
            file.Setup(f => f.FileName).Returns(filename);
            file.Setup(f => f.Length).Returns(bytes.Length);
            file.Setup(f => f.OpenReadStream()).Returns(stream);
            file.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default))
                .Returns((Stream s, System.Threading.CancellationToken _) =>
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    return stream.CopyToAsync(s);
                });

            return file;
        }
    }
}
