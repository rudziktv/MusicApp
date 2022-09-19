using MusicApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicApp.ViewModels
{
    internal class HomeViewModel : BaseViewModel
    {
        public ICommand FavPageCommand { get; set; }

        public HomeViewModel()
        {
            FavPageCommand = new Command(FavPage);
            
        }

        private async void FavPage(object obj)
        {
            await Shell.Current.GoToAsync("//FavPage");
        }
    }
}
