using MusicApp.ViewModels;
using MusicApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MusicApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
