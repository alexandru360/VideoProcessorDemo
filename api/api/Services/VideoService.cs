using Api.Helpers;
using DbEntities;
using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VideoProcessingTools;

namespace api.Services
{
    public interface IVideoService
    {
        Thumbnail GetById(int id);
        void Delete(int id);

        Task<List<ThumbnailDto>> CreateThumbnails(string urlFile);

        Task<string> CreateHls264(string urlFile);

        Task<string> CreateMultibitRate(string urlFile);
    }

    public class VideoService : IVideoService
    {
        private DataContext _context;

        public VideoService(DataContext context)
        {
            _context = context;
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


        public async Task<List<ThumbnailDto>> CreateThumbnails(string urlFile)
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

        public async Task<string> CreateHls264(string urlFile)
        {
            var hlsFile = VideoTool.GenerateHls264(urlFile);

            //Read the file and push contents to entity field
            var memory = new MemoryStream();
            var file = new FileInfoDto();
            var path = hlsFile.FilePath;
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
                memory.Position = 0;
                file.FileName = Path.GetFileName(path);
            }

            var hls264 = new Hls264()
            {
                FileName = file.FileName,
                FileContents = memory.ToArray()
            };
            _context.Hls264.Add(hls264);
            _context.SaveChanges();

            return $"Theere was a file entry in the Table Hls264 with ID# {hls264.Id}";
        }

        public async Task<string> CreateMultibitRate(string urlFile)
        {
            var ret = new List<string>();
            var hlsFiles = VideoTool.MultibitHls(urlFile);

            // Id = -1 it's a flag that I'm putting to know the group of the file name ... 
            // right now the group is hard coded but in the future will be something random (guid or something else)
            string groupName = hlsFiles.Where(w => w.Id == -1).FirstOrDefault().FileName;

            foreach (FileInfoDto itm in hlsFiles)
            {
                //Read the file and push contents to entity field
                var memory = new MemoryStream();
                var file = new FileInfoDto();
                var path = itm.FilePath;
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                    memory.Position = 0;
                    file.FileName = Path.GetFileName(path);
                }

                var mbitHls = new MultibitHls()
                {
                    FileName = file.FileName,
                    FileContents = memory.ToArray(),
                    GroupName = groupName,
                };
                _context.MultibitHls.Add(mbitHls);
                _context.SaveChanges();
                ret.Add(mbitHls.Id.ToString());
            }

            return $"Theere were several recod entries in the Table MultibitHls with IDs# {string.Join(",", ret)}";
        }
    }
}
