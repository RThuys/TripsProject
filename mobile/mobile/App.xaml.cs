using mobile.Bootstrap;
using mobile.Interfaces;
using mobile.Services;
using mobile.Services.Data;
using mobile.ViewModels;
using mobile.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace mobile
{
    public partial class App : Application
    {

        static SupervisorDataService dataService;
        public App()
        {
            InitializeComponent();

            InitializeApp();

            InitializeNavigation();
        }

        private void InitializeApp()
        {
            AppContainer.RegisterDependencies();

            
        }

        private async Task InitializeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        public static SupervisorDataService Database
        {
            get
            {
                if (dataService == null)
                {
                    dataService = new SupervisorDataService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Supervisors.db"));
                }
                return dataService;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
