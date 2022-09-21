using Android.Media;
using Android.Views.TextClassifiers;
using AngleSharp.Dom;
using MusicApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeExplode;
using YoutubeExplode.Converter;

namespace MusicApp.ViewModels
{
    class PlayerViewModel : BaseViewModel
    {
        public ICommand PlayPauseCommand { get; set; }
        public ICommand TimelineCommand { get; set; }
        public ICommand DownloadSongCommand { get; set; }
        public string HrefInput { get; set; }
        public string SongTitle { get; set; }
        public string SongAuthor { get; set; }

        private float _elapsedTime;

        public float ElapsedTime
        {
            get { return _elapsedTime; }
            set
            { 
                _elapsedTime = value;
                OnPropertyChanged(nameof(ElapsedTime));
            }
        }

        private string _playIconPath;

        public string PlayIconPath
        {
            get { return _playIconPath; }
            set
            { 
                _playIconPath = value;
                OnPropertyChanged(nameof(PlayIconPath));
            }
        }


        private float duration;

        Thread timeline;
        Thread download;


        private MediaPlayer player;

        public PlayerViewModel()
        {
            ElapsedTime = 0f;
            PlayPauseCommand = new Command(PlayPause);
            DownloadSongCommand = new Command(DownloadSong);
            TimelineCommand = new Command(Timeline);

            player = new MediaPlayer();
            player.Reset();
            //DownloadSong();
            //player.SetDataSource(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "music.mp3"));
            //player.Prepare();
            
            
            timeline = new Thread(TimelineUpdate);
            PlayIconPath = "play_line.png";
        }

        private void Timeline(object obj)
        {
            player.SeekTo((int)Math.Round(ElapsedTime * duration));
        }

        private async void DownloadSong()
        {
            timeline.Interrupt();
            if (!string.IsNullOrEmpty(HrefInput))
            {
                player.Reset();

                

                await Shell.Current.DisplayAlert("Download", "Download begin", "Ok");
                var youtube = new YoutubeDownloader();
                var yt = new YoutubeClient();
                var videoInfo = await yt.Videos.GetAsync(HrefInput);
                var fileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "music.mp4");
                await youtube.DownloadVideo(HrefInput, fileName);
                player.SetDataSource(fileName);
                player.Prepare();
                SongTitle = videoInfo.Title;
                SongAuthor = videoInfo.Author.ChannelTitle;
                OnPropertyChanged(nameof(SongTitle));
                OnPropertyChanged(nameof(SongAuthor));
                timeline.Start();
                await Shell.Current.DisplayAlert("Download", "Downloading file ended.", "Ok");
            }
        }

        private async void DownloadProcedure()
        {
            await Shell.Current.DisplayAlert("Download", "Download begin", "Ok");
            var youtube = new YoutubeDownloader();
            var yt = new YoutubeClient();
            var videoInfo = await yt.Videos.GetAsync(HrefInput);
            var fileName = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "music.mp4");
            await youtube.DownloadVideo(HrefInput, fileName);
            player.SetDataSource(fileName);
            player.Prepare();
            SongTitle = videoInfo.Title;
            SongAuthor = videoInfo.Author.ChannelTitle;
            OnPropertyChanged(nameof(SongTitle));
            OnPropertyChanged(nameof(SongAuthor));
            timeline.Start();
            await Shell.Current.DisplayAlert("Download", "Downloading file ended.", "Ok");
        }

        private void PlayPause()
        {
            if (player.IsPlaying)
            {
                PlayIconPath = "play_line.png";
                player.Pause();
            }
            else
            {
                PlayIconPath = "pause_line.png";
                player.Start();
            }
        }

        private async void TimelineUpdate()
        {
            duration = player.Duration;
            Device.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
            {
                float a = player.CurrentPosition;
                ElapsedTime = a / duration;
                PlayIconPath = player.IsPlaying ? "pause_line.png" : "play_line.png";
                return true;
            });
        }
    }
}
