using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace VideoProcessingTools
{
    public static class VideoTool
    {
        const string thumbDir = @"C:\temp";

        public static string generateThumb(string fileToProcess)
        {
            string sampleFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "4K-60FPS-1-min.mp4");

            if (fileToProcess.Length == 0) fileToProcess = sampleFile;
            string ffmpeg = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ffmpeg.exe");
            string ffmpegOutFile = Path.Combine(thumbDir, "ThumbnailOutput.png");

            try
            {
                if (!Directory.Exists(thumbDir))
                    Directory.CreateDirectory(thumbDir);

                string ffmpegCommand = $"{ffmpeg} -i {fileToProcess} -ss 1 -vframes 1 {ffmpegOutFile}";
                string ffmpegArguments = $"-i {fileToProcess} -ss 1 -vframes 1 {ffmpegOutFile}";

                using (var process = new Process())
                {
                    process.StartInfo.FileName = ffmpeg;
                    process.StartInfo.Arguments = ffmpegArguments;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;

                    process.OutputDataReceived += (sender, data) => Console.WriteLine(data.Data);
                    process.ErrorDataReceived += (sender, data) => Console.WriteLine(data.Data);
                    Console.WriteLine("starting");
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    Console.WriteLine($"exit {process.WaitForExit(5000)}");
                }
            }catch (Exception ex) { }

            if (File.Exists(ffmpegOutFile))
                return ffmpegOutFile;
            else return String.Empty;
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
