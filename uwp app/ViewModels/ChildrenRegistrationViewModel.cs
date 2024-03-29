﻿using Newtonsoft.Json;
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
        public string URL = "/Children";
        public string _FirstName { get; set; } = "First name";
        public string _LastName { get; set; } = "Last name";
        public string _Class { get; set; } = "testClass";
        private string _child;

        public ICommand RegisterChildCommand
        {
            get
            {
                return new CommandHandler(() => this.RegisterChild());
            }
        }

        private void RegisterChild()
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
            // MenuNavigationHelper.UpdateView(typeof(ChildDetailPage), child);
        }

     

        public void PostChild(string ChildJson)
        {
            DatabaseHelper.Post(URL, ChildJson);
        }

        public ChildrenRegistrationViewModel()
        {
        }



        private ICommand _viewChildDetailCommand;

        public ICommand ViewsChildrenOverviewCommand => _viewChildDetailCommand ?? (_viewChildDetailCommand = new RelayCommand(OnViewsChildDetail));
        
        
        private void OnViewsChildDetail() => MenuNavigationHelper.UpdateView(typeof(ChildDetailPage));
    }
}
