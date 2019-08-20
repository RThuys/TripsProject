using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using uwp.Model;
using uwp_app.Helpers;
using uwp_app.Services;
using uwp_app.Views;
using Windows.UI.Xaml.Controls;

namespace uwp_app.ViewModels
{
    public class TripsOverviewViewModel : Observable
    {
        private ICommand _viewPlanTripCommand;

        public TripsOverviewViewModel()
        {
            ApiCall("/Trips");
        }

        async void ApiCall(string url)
        {
            string pastTripsString = await DatabaseHelper.CreateClient(url+"/past");
            PastTrips = JsonConvert.DeserializeObject<List<Trip>>(pastTripsString);

            string futureTripsString = await DatabaseHelper.CreateClient(url + "/future");
            CommingTrips = JsonConvert.DeserializeObject<List<Trip>>(futureTripsString);

            string todaysTripsString = await DatabaseHelper.CreateClient(url + "/today");
            TodaysTrips = JsonConvert.DeserializeObject<List<Trip>>(todaysTripsString);
        }

        public ICommand ViewPlanTripCommand => _viewPlanTripCommand ?? (_viewPlanTripCommand = new RelayCommand(OnTripPlanOverview));

        private void OnTripPlanOverview() =>
            MenuNavigationHelper.UpdateView(typeof(TripPlannerPage));

        public void ClickItemList(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (Trip)e.ClickedItem;
            MenuNavigationHelper.UpdateView(typeof(TripDetailPage), clickedItem);
        }


        private List<Trip> _pastTrips;
        public List<Trip> PastTrips
        {
            get { return _pastTrips; }
            set { Set(ref _pastTrips, value, "PastTrips"); }

        }

        private List<Trip> _commingTrips;
        public List<Trip> CommingTrips
        {
            get { return _commingTrips; }
            set { Set(ref _commingTrips, value, "CommingTrips"); }

        }

        private List<Trip> _todaysTrips;
        public List<Trip> TodaysTrips
        {
            get { return _todaysTrips; }
            set { Set(ref _todaysTrips, value, "TodaysTrips"); }

        }
    }
}
