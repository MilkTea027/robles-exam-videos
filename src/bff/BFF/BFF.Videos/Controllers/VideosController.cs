using BFF.Videos.Data;
using BFF.Videos.Data.Entities;
using BFF.Videos.Repositories.Interfaces;
using BFF.Videos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BFF.Videos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideosController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var videos = await _videoService.GetAllAsync();
            return Ok(videos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Video video, IFormFile file)
        {
            var videoId = await _videoService.CreateAsync(video, file);
            return CreatedAtAction(nameof(Get), new { id = videoId }, videoId);
        }
    }
}