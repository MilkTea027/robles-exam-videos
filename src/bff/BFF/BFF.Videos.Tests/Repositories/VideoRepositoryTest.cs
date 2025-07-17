using BFF.Videos.Data;
using BFF.Videos.Data.Entities;
using BFF.Videos.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BFF.Videos.Tests.Repositories
{
    public class VideoRepositoryTest
    {
        private async Task<AppDbContext> GetDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_VideoDb")
                .Options;

            var context = new AppDbContext(options);

            // Add test data
            var category = new Category { Id = 1, Name = "Test Category" };
            context.Categories.Add(category);

            context.Videos.Add(new Video { Id = 1, Name = "Sample Video", CategoryId = 1, Category = category });
            await context.SaveChangesAsync();

            return context;
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnVideos_WithCategories()
        {
            var context = await GetDbContextAsync();
            var repository = new VideoRepository(context);

            var result = await repository.GetAllAsync();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.All(result, video =>
            {
                Assert.NotNull(video.Category);
                Assert.Equal("Test Category", video.Category.Name);
            });
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewVideo_AndReturnId()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Create_VideoDb")
                .Options;

            using var context = new AppDbContext(options);
            context.Categories.Add(new Category { Id = 2, Name = "Another Category" });
            await context.SaveChangesAsync();

            var repository = new VideoRepository(context);
            var newVideo = new Video
            {
                Name = "New Video",
                CategoryId = 2,
                Description = "A test video",
                File = "video.mp4",
                Thumbnail = "thumb.jpg",
                Size = 123456
            };

            var newId = await repository.CreateAsync(newVideo);

            Assert.True(newId > 0);
            var inserted = await context.Videos.FindAsync(newId);
            Assert.NotNull(inserted);
            Assert.Equal("New Video", inserted.Name);
        }
    }
}