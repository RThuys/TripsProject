using System;
using System.Collections.Generic;
using System.Windows.Input;

using uwp_app.Helpers;
using uwp_app.Services;
using uwp_app.Views;

using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace uwp_app.ViewModels
{
    public class ShellViewModel : Observable
    {
        private readonly KeyboardAccelerator _altLeftKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu);
        private readonly KeyboardAccelerator _backKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.GoBack);
        private IList<KeyboardAccelerator> _keyboardAccelerators;

        private ICommand _loadedCommand;
        private ICommand _menuViewsMainCommand;
        private ICommand _menuViewsChildDetailCommand;
        private ICommand _menuViewsChildrenOverviewCommand;
        private ICommand _menuViewsChildrenRegistrationCommand;
        private ICommand _menuViewsTripDetailCommand;
        private ICommand _menuViewsTripPlannerCommand;
        private ICommand _menuViewsTripsOverviewCommand;
        private ICommand _menuFileExitCommand;

        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

        public ICommand MenuViewsMainCommand => _menuViewsMainCommand ?? (_menuViewsMainCommand = new RelayCommand(OnMenuViewsMain));

        public ICommand MenuViewsChilDetailCommand => _menuViewsChildDetailCommand ?? (_menuViewsChildDetailCommand = new RelayCommand(OnMenuViewsChildrenOverview));

        public ICommand MenuViewsChildrenOverviewCommand => _menuViewsChildrenOverviewCommand ?? (_menuViewsChildrenOverviewCommand = new RelayCommand(OnMenuViewsChildrenOverview));

        public ICommand MenuViewsChildrenRegistrationCommand => _menuViewsChildrenRegistrationCommand ?? (_menuViewsChildrenRegistrationCommand = new RelayCommand(OnMenuViewsChildrenRegistration));

        public ICommand MenuViewsTripDetailCommand => _menuViewsTripDetailCommand ?? (_menuViewsTripDetailCommand = new RelayCommand(OnMenuViewsTripDetail));

        public ICommand MenuViewsTripPlannerCommand => _menuViewsTripPlannerCommand ?? (_menuViewsTripPlannerCommand = new RelayCommand(OnMenuViewsTripPlanner));

        public ICommand MenuViewsTripsOverviewCommand => _menuViewsTripsOverviewCommand ?? (_menuViewsTripsOverviewCommand = new RelayCommand(OnMenuViewsTripsOverview));

        public ICommand MenuFileExitCommand => _menuFileExitCommand ?? (_menuFileExitCommand = new RelayCommand(OnMenuFileExit));

        public ShellViewModel()
        {
        }

        public void Initialize(Frame shellFrame, SplitView splitView, Frame rightFrame, IList<KeyboardAccelerator> keyboardAccelerators)
        {
            NavigationService.Frame = shellFrame;
            MenuNavigationHelper.Initialize(splitView, rightFrame);
            _keyboardAccelerators = keyboardAccelerators;
        }

        private void OnLoaded()
        {
            // Keyboard accelerators are added here to avoid showing 'Alt + left' tooltip on the page.
            // More info on tracking issue https://github.com/Microsoft/microsoft-ui-xaml/issues/8
            _keyboardAccelerators.Add(_altLeftKeyboardAccelerator);
            _keyboardAccelerators.Add(_backKeyboardAccelerator);
        }

        private void OnMenuViewsMain() => MenuNavigationHelper.UpdateView(typeof(MainPage));
  
        private void OnMenuViewsChildDetail() => MenuNavigationHelper.UpdateView(typeof(ChildDetailPage));

        private void OnMenuViewsChildrenOverview() => MenuNavigationHelper.UpdateView(typeof(ChildrenOverviewPage));

        private void OnMenuViewsChildrenRegistration() => MenuNavigationHelper.UpdateView(typeof(ChildrenRegistrationPage));

        private void OnMenuViewsTripDetail() => MenuNavigationHelper.UpdateView(typeof(TripDetailPage));

        private void OnMenuViewsTripPlanner() => MenuNavigationHelper.UpdateView(typeof(TripPlannerPage));

        private void OnMenuViewsTripsOverview() => MenuNavigationHelper.UpdateView(typeof(TripsOverviewPage));

        private void OnMenuFileExit()
        {
            Application.Current.Exit();
        }

        private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
        {
            var keyboardAccelerator = new KeyboardAccelerator() { Key = key };
            if (modifiers.HasValue)
            {
                keyboardAccelerator.Modifiers = modifiers.Value;
            }

            keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;
            return keyboardAccelerator;
        }

        private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            var result = NavigationService.GoBack();
            args.Handled = result;
        }
    }
}
