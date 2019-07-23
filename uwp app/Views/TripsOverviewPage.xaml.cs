using System;

using uwp_app.ViewModels;

using Windows.UI.Xaml.Controls;

namespace uwp_app.Views
{
    public sealed partial class TripsOverviewPage : Page
    {
        public TripsOverviewViewModel ViewModel { get; } = new TripsOverviewViewModel();

        public TripsOverviewPage()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }
    }
}
