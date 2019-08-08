using mobile.Interfaces;
using mobile.Models;
using mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripsPage : ContentPage
    {
        //private TripsPageModelView tripsPageModelView = new TripsPageModelView(INavigationService);

        public TripsPage()
        {
            InitializeComponent();
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //tripsPageModelView.OnListViewItemSelected((Trip)e.SelectedItem);
        }
    }
}