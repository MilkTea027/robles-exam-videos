using BFF.Videos.Data.Entities;
using BFF.Videos.Repositories.Interfaces;
using BFF.Videos.Services.Interfaces;

namespace BFF.Videos.Services
{
    public class VideoService : IVideoService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IVideoRepository _repository;

        public VideoService(
            IWebHostEnvironment env,
            IVideoRepository repository)
        {
            _env = env;
            _repository = repository;
        }

        public async Task<IEnumerable<Video>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<int> CreateAsync(Video video, IFormFile file)
        {
            var filename = Path.GetExtension(file.FileName).ToLowerInvariant();
            ValidateVideoFile(file, filename);

            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploads);

            var uniqueName = $"{Guid.NewGuid()}{filename}";
            var filePath = Path.Combine(uploads, uniqueName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            video.File = uniqueName;
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