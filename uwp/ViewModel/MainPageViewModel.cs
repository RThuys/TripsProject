using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using CommonServiceLocator;

using GalaSoft.MvvmLight.Views;

using uwp.Services;

using Windows.UI.Xaml.Controls;

namespace uwp.ViewModel
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        //[NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand NavigateToChildrenOverviewPage
        {
            get
            {
                return NavigationBuilder(App.ChildrenOverviewPage);
            }
        }

        public ICommand NavigateToChildrenPage
        {
            get
            {
                return NavigationBuilder(App.ChildRegistrationPage);
            }
        }
   
        public ICommand NavigateToTripDetailPage
        {
            get
            {
                return NavigationBuilder(App.TripDetailPage);
            }
        }

        public ICommand NavigateToTripPlannerPage
        {
            get
            {
                return NavigationBuilder(App.TripPlannerPage);
            }
        }

        public ICommand NavigateToTripOverviewPage
        {
            get
            {
                return  NavigationBuilder(App.TripsOverviewPage);
            }
        }

        private CommandActionHandler NavigationBuilder(string arg)
        {
            return new CommandActionHandler(() =>
                   {
                       this.test(arg);
                   });
        }

        private void test(string arg)
        {
            var navigation = ServiceLocator.Current.GetInstance<INavigationService>();
            navigation.NavigateTo(arg);
        }


        
    }
}
