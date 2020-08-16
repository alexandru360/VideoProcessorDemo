using api.Services;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoToolsController : ControllerBase
    {

        private readonly ILogger<VideoToolsController> _logger;
        private IVideoService _videoService;

        public VideoToolsController(
            IVideoService VideoService,
            ILogger<VideoToolsController> logger)
        {
            _logger = logger;
            _videoService = VideoService;
        }

        [HttpGet("health-check")]
        public IActionResult HealthCheck()
        {
            return Ok("VideoTools -> UP");
        }

        [HttpPost("hd-264")]
        public async Task<IActionResult> HdFile()
        {
            try
            {
                var ret = await _videoService.CreateHls264(string.Empty);
                return Ok(new MessageDto
                {
                    Content = ret
                }); ;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("thumbnails")]
        [AllowAnonymous]
        public async Task<IActionResult> Thumbnails()
        {
            try
            {
                var ret = await _videoService.CreateThumbnails(string.Empty);
                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("hls-files")]
        public async Task<IActionResult> HlsFiles()
        {
            try
            {
                var ret = await _videoService.CreateMultibitRate(string.Empty);
                return Ok(new MessageDto
                {
                    Content = ret
                }); ;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
