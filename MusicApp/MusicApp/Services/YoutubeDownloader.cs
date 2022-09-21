using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;

namespace MusicApp.Services
{
    internal class YoutubeDownloader
    {
        public async ValueTask DownloadVideo(string url, string filePath)
        {
            var source = filePath;
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(url);
            File.WriteAllBytes(source, vid.GetBytes());
        }
    }
}
