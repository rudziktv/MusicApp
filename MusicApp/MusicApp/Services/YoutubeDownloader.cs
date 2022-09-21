using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VideoLibrary;

namespace MusicApp.Services
{
    internal class YoutubeDownloader
    {
        public void DownloadVideo(string url, string filePath)
        {
            var source = filePath;
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(url);
            File.WriteAllBytes(source, vid.GetBytes());
        }
    }
}
