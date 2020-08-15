using api.Helpers;
using Api.Helpers;
using DbEntities;
using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VideoProcessingTools;

namespace api.Services
{
    public interface IVideoService
    {
        Thumbnail GetById(int id);
        Task<List<ThumbnailDto>> Create(string urlFile);
        void Delete(int id);
    }

    public class VideoService : IVideoService
    {
        private DataContext _context;

        public VideoService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ThumbnailDto>> Create(string urlFile)
        {
            List<ThumbnailDto> oRet = new List<ThumbnailDto>();
            Thumbnail thumbnail;
            var oThumb = VideoTool.GenerateThumbnails(urlFile);

            //Read the file and push contents to list
            var memory = new MemoryStream();
            var thumb = new ThumbnailDto();
            var path = oThumb.ThumbnailOnePath;
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
                memory.Position = 0;
                thumb.FileName = Path.GetFileName(path);
                thumb.FileContents = Convert.ToBase64String(memory.ToArray());
            }

            thumbnail = new Thumbnail()
            {
                FileName = thumb.FileName,
                FileContents = memory.ToArray()
            };
            _context.Thumbnails.Add(thumbnail);
            _context.SaveChanges();

            thumb.Id = thumbnail.Id;
            oRet.Add(thumb);

            //Read the file and push contents to list
            memory = new MemoryStream();
            thumb = new ThumbnailDto();
            path = oThumb.ThumbnailTwoPath;
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
                memory.Position = 0;
                thumb.FileName = Path.GetFileName(path);
                thumb.FileContents = Convert.ToBase64String(memory.ToArray());
            }

            thumbnail = new Thumbnail()
            {
                FileName = thumb.FileName,
                FileContents = memory.ToArray()
            };
            _context.Thumbnails.Add(thumbnail);
            _context.SaveChanges();

            thumb.Id = thumbnail.Id;
            oRet.Add(thumb);

            return oRet;
        }

        public void Delete(int id)
        {
            var trip = _context.Thumbnails.Find(id);
            if (trip != null)
            {
                _context.Thumbnails.Remove(trip);
                _context.SaveChanges();
            }
        }

        public Thumbnail GetById(int id)
        {
            return _context.Thumbnails.Find(id);
        }

    }
}
