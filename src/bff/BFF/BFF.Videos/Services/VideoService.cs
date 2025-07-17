using BFF.Videos.Data.Entities;
using BFF.Videos.Repositories.Interfaces;
using BFF.Videos.Services.Interfaces;

namespace BFF.Videos.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _repository;

        public VideoService(IVideoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Video>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<int> CreateAsync(Video video, IFormFile file, IFormFile thumbnail)
        {
            var videoExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            ValidateVideoFile(file, videoExtension);

            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","uploads");
            Directory.CreateDirectory(uploads);

            var videoFileName = $"{Guid.NewGuid()}{videoExtension}";
            var videoFilePath = Path.Combine(uploads, videoFileName);

            
            using (var stream = new FileStream(videoFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var thumbnailFileName = $"{Guid.NewGuid()}{Path.GetExtension(thumbnail.FileName)}";
            var thumbnailFilePath = Path.Combine(uploads, thumbnailFileName);

            using (var stream = new FileStream(thumbnailFilePath, FileMode.Create))
            {
                await thumbnail.CopyToAsync(stream);
            }

            video.Thumbnail = thumbnailFileName;
            video.File = videoFileName;
            video.Size = file.Length;

            return await _repository.CreateAsync(video);
        }

        public void ValidateVideoFile(IFormFile file, string filename)
        {
            if (file == null || file.Length == 0)
                throw new BadHttpRequestException("No file uploaded");

            if (file.Length > 100 * 1024 * 1024)
                throw new BadHttpRequestException("File exceeds 100MB limit");

            if (!new[] { ".mp4", ".avi", ".mov" }.Contains(filename))
                throw new BadHttpRequestException("Invalid file type");
        }
    }
}