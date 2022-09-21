using Android.Media;
using MusicApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeExplode;
using YoutubeExplode.Converter;

namespace MusicApp.ViewModels
{
    class PlayerViewModel : BaseViewModel
    {
        public ICommand PlayPauseCommand { get; set; }

        private MediaPlayer player;

        public PlayerViewModel()
        {
            PlayPauseCommand = new Command(PlayPause);

            player = new MediaPlayer();
            player.Reset();
            DownloadSong();
            //player.SetDataSource(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "music.mp3"));
            //player.Prepare();
            var fileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "music.mp4");
            player.SetDataSource(fileName);
            player.Prepare();
        }

        private async void DownloadSong()
        {
            var youtube = new YoutubeDownloader();
            var fileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "music.mp4");
            youtube.DownloadVideo(@"https://www.youtube.com/watch?v=EDrOriKrV3g", fileName);
        }

        private void PlayPause()
        {
            if (player.IsPlaying)
            {
                player.Pause();
            }
            else
            {
                player.Start();
            }
        }
    }
}
