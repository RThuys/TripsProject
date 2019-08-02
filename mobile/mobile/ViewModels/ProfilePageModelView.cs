using mobile.Interfaces;
using mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobile.ViewModels
{
    public class ProfilePageModelView: ViewModelBase
    {

        private string _supervisorName;


        public ProfilePageModelView(INavigationService navigationService
            ) : base(navigationService)
        {
            SupervisorName = App.Database.GetSupervisorsAsync().ToString();
            Console.WriteLine();
        }

        public string SupervisorName
        {
            get => _supervisorName;
            set
            {
                _supervisorName = value;
                OnPropertyChanged();
            }
        }
    }
}
