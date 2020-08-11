using api.Helpers;
using api.Services;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VideoProcessingTools;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ThumbnailController : ControllerBase
    {

        private readonly ILogger<ThumbnailController> _logger;
        private IVideoService _videoService;

        public ThumbnailController(
            IVideoService VideoService,
            ILogger<ThumbnailController> logger)
        {
            _logger = logger;
            _videoService = VideoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                ThumbnailDto thumbnail = await _videoService.Create();
                //return File(thumbnail.FileContents, Tools.GetContentType(thumbnail.FilePath), thumbnail.FileName);
                return Ok(thumbnail);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
