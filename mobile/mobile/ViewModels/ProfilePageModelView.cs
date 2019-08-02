using mobile.Interfaces;
using mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class ProfilePageModelView: ViewModelBase
    {

        private string _supervisorName;
        private INavigationService _navigationService;

        List<Supervisor> test;
        public ProfilePageModelView(INavigationService navigationService
            ) : base(navigationService)
        {
            _navigationService = navigationService;
            test= App.Database.GetSupervisorsAsync().Result;
            SupervisorName = test[0].Name;
        }

        public string SupervisorName
        {
            get =>  _supervisorName;
            set
            {
                _supervisorName = value;
                OnPropertyChanged();
            }
        }

        public ICommand LogoutCommand => new Command(Logout);
        public ICommand EditUsernameCommand => new Command(EditUserName);

        private async void Logout()
        {
            await App.Database.DeletenoteAsync(test[0]);
            await _navigationService.NavigateToAsync<MainPageViewModel>();
        }

        private void EditUserName()
        {

        }
    }
}
