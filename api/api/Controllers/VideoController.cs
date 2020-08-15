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
        public IActionResult HdFile([FromBody] string content)
        {
            try
            {
                return Ok("hd-264");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("thumbnails")]
        [AllowAnonymous]
        public async Task<IActionResult> Thumbnails([FromBody] string content)
        {
            try
            {
                if (content.Length == 0)
                    content = string.Empty;
                var ret = await _videoService.Create(content);
                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("hls-files")]
        public async Task<IActionResult> HlsFiles([FromBody] string content)
        {
            try
            {
                return Ok("hls-files");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
