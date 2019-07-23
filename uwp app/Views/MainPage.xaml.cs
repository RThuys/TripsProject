using System;

using uwp_app.ViewModels;

using Windows.UI.Xaml.Controls;

namespace uwp_app.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
