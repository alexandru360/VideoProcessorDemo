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
        Task<ThumbnailDto> Create();
        void Delete(int id);
    }

    public class VideoService : IVideoService
    {
        private DataContext _context;

        public VideoService(DataContext context)
        {
            _context = context;
        }

        public async Task<ThumbnailDto> Create()
        {
            var path = VideoTool.generateThumb(string.Empty);

            var memory = new MemoryStream();
            var ret = new ThumbnailDto();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);

                memory.Position = 0;
                var fileName = Path.GetFileName(path);
                Thumbnail thumbnail = new Thumbnail();
                thumbnail.FileName = fileName;
                thumbnail.FileContents = memory.ToArray();
                _context.Thumbnails.Add(thumbnail);
                _context.SaveChanges();



                ret.FileName = fileName;
                ret.FilePath = path;
                //ret.FileContents = Tools.Base64Encode(memory.ToArray().ToString());
                ret.FileContents = Convert.ToBase64String(memory.ToArray()); ;
                
            }

            return ret;
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
