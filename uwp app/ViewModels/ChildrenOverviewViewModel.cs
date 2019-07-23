using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using uwp.Model;
using uwp_app.Helpers;
using uwp_app.Services;
using uwp_app.Views;
using Windows.UI.Xaml.Controls;

namespace uwp_app.ViewModels
{
    public class ChildrenOverviewViewModel : Observable
    {
        private ICommand _viewRegisterChildCommand;

        private List<Child> _children;
        public List<Child> Children
        {
            get { return _children; }
            set { Set(ref _children, value, "Children"); }

        }
        public ChildrenOverviewViewModel()
        {
            ApiCall("/Children");
        }

        async void ApiCall(string url)
        {

            string response = await DatabaseHelper.CreateClient(url);
            var data = JsonConvert.DeserializeObject<List<Child>>(response);
            Children = data;
        }

        public ICommand ViewRegisterChildCommand => _viewRegisterChildCommand ?? (_viewRegisterChildCommand = new RelayCommand(OnChildRegisterOverview));

        private void OnChildRegisterOverview() => MenuNavigationHelper.UpdateView(typeof(ChildrenRegistrationPage));

        public void ClickItemList(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (Child)e.ClickedItem;
            MenuNavigationHelper.UpdateView(typeof(ChildDetailPage), clickedItem);
        }
    }
}
