using System;
using System.Windows.Input;
using uwp_app.Helpers;
using uwp_app.Views;

namespace uwp_app.ViewModels
{
    public class MainViewModel : Observable
    {

        private ICommand _viewsChildrenOverviewCommand;
        private ICommand _viewsTripsOverviewCommand;


        public ICommand ViewsChildrenOverviewCommand => _viewsChildrenOverviewCommand ?? (_viewsChildrenOverviewCommand = new RelayCommand(OnViewsChildrenOverview));
        public ICommand ViewsTripsOverviewCommand => _viewsTripsOverviewCommand ?? (_viewsTripsOverviewCommand = new RelayCommand(OnViewsTripsOverview));

        public MainViewModel()
        {

        }
        private void OnViewsChildrenOverview() => MenuNavigationHelper.UpdateView(typeof(ChildrenOverviewPage));
        private void OnViewsTripsOverview() => MenuNavigationHelper.UpdateView(typeof(TripsOverviewPage));

    }
}
