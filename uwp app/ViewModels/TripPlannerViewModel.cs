using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using uwp.Model;
using uwp_app.Helpers;
using uwp_app.Model;
using uwp_app.Services;
using uwp_app.Views;
using Windows.UI.Xaml.Controls;

namespace uwp_app.ViewModels
{
    public class TripPlannerViewModel : Observable
    {
        static Supervisor superv;
        public string URL = "/Trips";
        public string _Title { get; set; } = "Title";
        public Supervisor _Supervisor { get; set; } = superv ;
        public DateTime _Date { get; set; } = DateTime.Now;

        private string _trip;

        public TripPlannerViewModel()
        {
            ApiCall("/Supervisors");
        }

        public ICommand RegisterTrip
        {
            get
            {
                return new CommandHandler(() => this.test());
            }
        }

        //TODO change to later date
        private void test()
        {

            Trip trip = new Trip
            {
                Title = _Title,
                SupervisorId = _Supervisor.Id,
                Date = _Date
            };
            _trip = Newtonsoft.Json.JsonConvert.SerializeObject(trip);
            PostTrip(_trip);
            MenuNavigationHelper.UpdateView(typeof(MainPage));
        }


        public void PostTrip(string tripJson)
        {
            DatabaseHelper.Post(URL, tripJson);
        }

        public DateTimeOffset BindDate
        {
            get
            {
                return DateTimeOffset.Now;
            }
        }

        private List<Supervisor> _supervisors;
        public List<Supervisor> Supervisors
        {
            get { return _supervisors; }
            set { Set(ref _supervisors, value, "Supervisors"); }

        }

        async void ApiCall(string url)
        {

            string response = await DatabaseHelper.CreateClient(url);
            var data = JsonConvert.DeserializeObject<List<Supervisor>>(response);
            Supervisors = data;
            superv = data[1];
        }
    }
}
