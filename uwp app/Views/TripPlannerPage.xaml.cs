using System;
using uwp_app.Model;
using uwp_app.ViewModels;

using Windows.UI.Xaml.Controls;

namespace uwp_app.Views
{
    public sealed partial class TripPlannerPage : Page
    {
        public TripPlannerViewModel ViewModel { get; } = new TripPlannerViewModel();

        public TripPlannerPage()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        //TODO clean up
        private void SupervisorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel._Supervisor = (sender as ComboBox).SelectedItem as Supervisor;
            Console.WriteLine();
        }
    }
}
