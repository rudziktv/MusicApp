using MusicApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MusicApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}