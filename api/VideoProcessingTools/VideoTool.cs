using DTO;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace VideoProcessingTools
{
    public static class VideoTool
    {
        const string thumbDir = @"C:\temp";

        public static ThumbnailPathsDto GenerateThumbnails(string fileToProcess)
        {
            var oRet = new ThumbnailPathsDto();
            string sampleFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Sample", "4K-video-30-secs.mp4");
            if (fileToProcess.Length == 0) fileToProcess = sampleFile;
            string ffmpeg = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ffmpeg.exe");

            if (!Directory.Exists(thumbDir))
                Directory.CreateDirectory(thumbDir);

            string ffmpegOutFile = Path.Combine(thumbDir, $"output-1.png");
            string ffmpegArguments = $"-ss 00:00:01 -i {fileToProcess} -ss 1 -vframes 1 {ffmpegOutFile}";
            var process = VideoExternalProcess(ffmpeg, ffmpegArguments);
            if (File.Exists(ffmpegOutFile))
                oRet.ThumbnailOnePath = ffmpegOutFile;

            ffmpegOutFile = Path.Combine(thumbDir, $"output-2.png");
            ffmpegArguments = $"-ss 00:00:05 -i {fileToProcess} -ss 1 -vframes 1 {ffmpegOutFile}";
            process = VideoExternalProcess(ffmpeg, ffmpegArguments);
            if (File.Exists(ffmpegOutFile))
                oRet.ThumbnailTwoPath = ffmpegOutFile;

            if (oRet.ThumbnailOnePath.Length > 0 && oRet.ThumbnailTwoPath.Length > 0)
                return oRet;
            else return null;
        }

        private static bool VideoExternalProcess(string cmd, string args)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = cmd;
                    process.StartInfo.Arguments = args;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;

                    process.OutputDataReceived += (sender, data) => Console.WriteLine(data.Data);
                    process.ErrorDataReceived += (sender, data) => Console.WriteLine(data.Data);
                    Console.WriteLine("Starting process ...");
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    Console.WriteLine($"Process exit #{process.WaitForExit(5000)}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CreateDirectory(string dirPathAndName)
        {
            bool ret = true;

            try
            {
                if (!Directory.Exists(dirPathAndName))
                    Directory.CreateDirectory(dirPathAndName);
            }
            catch (Exception ex)
            {
                ret = false;
                // Do something with the exception here ... log it maybe
            }

            return ret;
        }
    }
}
