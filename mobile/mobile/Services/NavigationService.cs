using mobile.Bootstrap;
using mobile.Interfaces;
using mobile.Models;
using mobile.ViewModels;
using mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mobile.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication => Application.Current;

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public Task ClearBackStack()
        {
            throw new NotImplementedException();
        }

        public async Task InitializeAsync()
        {
            List<Supervisor> test = App.Database.GetSupervisorsAsync().Result;
            var loggedIn = (test.Count == 0 ? false : true);
            if (loggedIn == true)
            {
                await NavigateToAsync<HomePageViewModel>();
            }
            else
            {
                await NavigateToAsync<MainPageViewModel>();
            }
        }

        public async Task NavigateBackAsync()
        {
            await CurrentApplication.MainPage.Navigation.PopAsync();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public async Task NavigateToAsync(Page page)
        {
            var navigationPage = CurrentApplication.MainPage as ProjNavigationPage;
            await navigationPage.PushAsync(page);
        }

        public Task PopToRootAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveLastFromBackStackAsync()
        {
            throw new NotImplementedException();
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            var navigationPage = CurrentApplication.MainPage as ProjNavigationPage;

            if (navigationPage != null && !(page is HomePage))
            {
                await navigationPage.PushAsync(page);
            }
            else
            {
                CurrentApplication.MainPage = new ProjNavigationPage(page);
            }
            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(HomePageViewModel), typeof(HomePage));
            _mappings.Add(typeof(MainPageViewModel), typeof(MainPage));
            _mappings.Add(typeof(ChildrenPageModelView), typeof(ChildrenPage));
            _mappings.Add(typeof(ProfilePageModelView), typeof(ProfilePage));
            _mappings.Add(typeof(TripsDetailPageModelView), typeof(TripsDetailPage));
            _mappings.Add(typeof(TripsPageModelView), typeof(TripsPage));
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null) {
                throw new Exception($"This: {viewModelType} is not a page");
            } else
            {
                Page page = Activator.CreateInstance(pageType) as Page;
                ViewModelBase viewModel = AppContainer.Resolve(viewModelType) as ViewModelBase;
                page.BindingContext = viewModel;

                return page;
            }
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"no navigation maps were found for ${viewModelType}");
            }
            return _mappings[viewModelType];
        }
    }
}
