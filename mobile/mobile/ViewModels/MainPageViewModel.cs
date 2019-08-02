using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using mobile.Interfaces;
using mobile.Models;
using mobile.Services.Data;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class MainPageViewModel: ViewModelBase
    {

        private string _userName;
        private string _errorLabel;
        

        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public ICommand LoginCommand => new Command(OnClickLogin);

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public string ErrorLabel
        {
            get => _errorLabel;
            set
            {
                _errorLabel = value;
                OnPropertyChanged();
            }
        }

        private async void OnClickLogin()
        {
            if (_userName != null)
            {
                var supervisor = new Supervisor();
                supervisor.Name = _userName;
                await App.Database.SaveSupervisorAsync(supervisor);
                await _navigationService.NavigateToAsync<HomePageViewModel>();
            }
            else
                ErrorLabel = "Please fill a in a username";
        }
    }
}
