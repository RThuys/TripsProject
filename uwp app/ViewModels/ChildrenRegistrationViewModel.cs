using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using uwp.Model;
using uwp_app.Helpers;
using uwp_app.Services;
using uwp_app.Views;

namespace uwp_app.ViewModels
{
    public class ChildrenRegistrationViewModel : Observable
    {
        private ICommand _viewChildDetailCommand;

        public string URL = "/Children";
        public string _FirstName { get; set; } = "First name";
        public string _LastName { get; set; } = "Last name";
        public string _Class { get; set; } = "testClass";
        private string _child;

        private Child child;

        public ChildrenRegistrationViewModel()
        {
        }

        public ICommand RegisterChildCommand => new CommandHandler(() => this.RegisterChild());

        private async void RegisterChild()
        {
            Child child = new Child
            {
                Name = _FirstName,
                LastName = _LastName,
                Class = _Class
            };
            _child = Newtonsoft.Json.JsonConvert.SerializeObject(child);
            PostChild(_child);
            MenuNavigationHelper.UpdateView(typeof(MainPage));

            //TODO can't seem to get last added value from database, GetMethod (await) is alwats skipped
            //MenuNavigationHelper.UpdateView(typeof(ChildDetailPage), child);
        }


        public async void PostChild(string ChildJson)
        {
            DatabaseHelper.Post(URL, ChildJson);
        }

        public ICommand ViewsChildrenOverviewCommand => _viewChildDetailCommand ?? (_viewChildDetailCommand = new RelayCommand(OnViewsChildDetail));
        
        
        private void OnViewsChildDetail() => MenuNavigationHelper.UpdateView(typeof(ChildDetailPage));
    }
}
