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

            string response = await DatabaseHelper.CreateClient(url);

            var data = JsonConvert.DeserializeObject<List<Trip>>(response);
            List<Trip> pastTrips = new List<Trip>();
            List<Trip> upcommingTrips = new List<Trip>();
            foreach (var trip in data) {
                if (trip.Date.Date < DateTime.Now.Date)
                {
                    pastTrips.Add(trip);
                } else
                {
                    upcommingTrips.Add(trip);
                }
            }
            if (pastTrips.Count != 0) PastTrips = pastTrips;
            if (upcommingTrips.Count != 0) CommingTrips = upcommingTrips;
        }

        public ICommand ViewPlanTripCommand => _viewPlanTripCommand ?? (_viewPlanTripCommand = new RelayCommand(OnTripPlanOverview));

        private void OnTripPlanOverview() =>
            MenuNavigationHelper.UpdateView(typeof(TripPlannerPage));

        public void ClickItemListToCome(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (Trip)e.ClickedItem;
            MenuNavigationHelper.UpdateView(typeof(TripDetailPage), clickedItem);
        }

        public void ClickItemListPast(object sender, ItemClickEventArgs e)
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
    }
}
