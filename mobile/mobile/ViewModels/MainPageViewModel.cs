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

        private readonly ISupervisorDataService _supervisorDataService;


        public MainPageViewModel(INavigationService navigationService, ISupervisorDataService supervisorDataService) : base(navigationService)
        {
            _supervisorDataService = supervisorDataService;
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
        private string _supervisor;

        private async void OnClickLogin()
        {
            if (_userName != null)
            {
                var supervisor = new Supervisor();
                supervisor.Name = _userName;
                await App.Database.SaveSupervisorAsync(supervisor);
                
                    Supervisor supervisorObject = new Supervisor
                    {
                        Name = _userName
                    };
            _supervisor = Newtonsoft.Json.JsonConvert.SerializeObject(supervisorObject);
                    PostSupervisor(_supervisor);
               
                await _navigationService.NavigateToAsync<HomePageViewModel>();
            }
            else
                ErrorLabel = "Please fill a in a username";
        }

        public void PostSupervisor(string tripSupervisor)
        {
            _supervisorDataService.AddSupervisor(tripSupervisor);
        }
    }
}
