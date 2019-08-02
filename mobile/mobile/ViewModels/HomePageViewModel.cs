using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using mobile.Interfaces;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class HomePageViewModel: ViewModelBase
    {
        private HomePageViewModel _homePageViewModel;

        public HomePageViewModel(INavigationService navigationService
            ): base(navigationService)
        {
             
        }

        public ICommand Childrencommand => new Command(GoToChildren);
        public ICommand TripsCommand => new Command(GoToTrips);
        public ICommand ProfileCommand => new Command(GoToProfile);

        private void GoToChildren()
        {
            _navigationService.NavigateToAsync<ChildrenPageModelView>();
        }

        private void GoToTrips()
        {
            _navigationService.NavigateToAsync<TripsPageModelView>();
        }

        private void GoToProfile()
        {
            _navigationService.NavigateToAsync<ProfilePageModelView>();
        }



        public override async Task InitializeAsync(object data)
        {
            await Task.WhenAll
            (
                //_navigationService.NavigateToAsync<HomeViewModel>()
            );
        }
    }
}
