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
        public ICommand SaveCommand => new Command(Save);
        public ICommand CancelCommand => new Command(Cancel);

        private async void Logout()
        {
            await App.Database.DeletenoteAsync(test[0]);
            await _navigationService.NavigateToAsync<MainPageViewModel>();
        }

        private bool _isLogoutVisible = true;

        public bool IsLogoutVisible { get
            {
                return _isLogoutVisible;
            }
            set
            {
                if (_isLogoutVisible != value)
                {
                    _isLogoutVisible = value;
                    OnPropertyChanged("IsLogoutVisible");
                }
            }
        }

        private void EditUserName()
        {
            IsLogoutVisible = false;
            IsCancelvisible = true;
            IsSaveVisible = true;
            IsEditButtonVisible = false;
            IsNameLabelVisible = false;
            IsNameEntryVisible = true;
            GetWelcomeText = "Edit your username below";
        }

        private bool _isEditButtonVisible = true;

        public bool IsEditButtonVisible
        {
            get
            {
                return _isEditButtonVisible;
            }
            set
            {
                if (_isEditButtonVisible != value)
                {
                    _isEditButtonVisible = value;
                    OnPropertyChanged("IsEditButtonVisible");
                }
            }
        }

        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        private async void Save()
        {
            if (_userName != null)
            {
                test[0].Name = _userName;
                await App.Database.UpdateSuperVisor(test[0]);
                SupervisorName = test[0].Name;
                Cancel();
                
            }
            else;
        }

        private bool _isSaveVisible = false;

        public bool IsSaveVisible
        {
            get
            {
                return _isSaveVisible;
            }
            set
            {
                if (_isSaveVisible != value)
                {
                    _isSaveVisible = value;
                    OnPropertyChanged("IsSaveVisible");
                }
            }
        }

        private void Cancel()
        {
            IsLogoutVisible = true;
            IsCancelvisible = false;
            IsSaveVisible = false;
            IsEditButtonVisible = true;
            IsNameLabelVisible = true;
            IsNameEntryVisible = false;
            GetWelcomeText = "Welcome to your profile!";
                }

        private bool _isCancelvisible = false;

        public bool IsCancelvisible
        {
            get
            {
                return _isCancelvisible;
            }
            set
            {
                if (_isCancelvisible != value)
                {
                    _isCancelvisible = value;
                    OnPropertyChanged("IsCancelvisible");
                }
            }
        }

        private bool _isNameLabelVisible = true;

        public bool IsNameLabelVisible
        {
            get
            {
                return _isNameLabelVisible;
            }
            set
            {
                if (_isNameLabelVisible != value)
                {
                    _isNameLabelVisible = value;
                    OnPropertyChanged("IsNameLabelVisible");
                }
            }
        }

        private bool _isNameEntryVisible = false;

        public bool IsNameEntryVisible
        {
            get
            {
                return _isNameEntryVisible;
            }
            set
            {
                if (_isNameEntryVisible != value)
                {
                    _isNameEntryVisible = value;
                    OnPropertyChanged("IsNameEntryVisible");
                }
            }
        }

        private string _welcomText = "Welcome to your profile!";

        public string GetWelcomeText
        {
            get
            {
                return _welcomText;
            }
            set
            {
                _welcomText = value;
                OnPropertyChanged("GetWelcomeText");
            }
        }




    }
}
