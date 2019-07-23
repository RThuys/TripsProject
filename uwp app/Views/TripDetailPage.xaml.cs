using System;
using uwp.Model;
using uwp_app.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace uwp_app.Views
{
    public sealed partial class TripDetailPage : Page
    {
        public TripDetailViewModel ViewModel { get; } = new TripDetailViewModel();

        public TripDetailPage()
        {
            InitializeComponent();
        }

        protected  override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.InitializeTripAsync((Trip)e.Parameter);
            this.DataContext = ViewModel;
        }
    }
}
