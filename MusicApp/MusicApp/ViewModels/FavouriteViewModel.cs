using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicApp.ViewModels
{
    class FavouriteViewModel : BaseViewModel
    {
        public ICommand HomePageCommand { get; set; }
        public ICommand LibraryPageCommand { get; set; }

        public FavouriteViewModel()
        {
            HomePageCommand = new Command(HomePage);
            LibraryPageCommand = new Command(LibraryPage);
        }

        private async void HomePage(object obj)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

        private async void LibraryPage(object obj)
        {
            await Shell.Current.GoToAsync("//LibPage");
        }
    }
}
