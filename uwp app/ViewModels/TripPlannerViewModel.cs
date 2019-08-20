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
        public DateTimeOffset _Date = DateTimeOffset.Now;
        public string _testdate ="";

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



        //TODO hour not correct
        private void test()
        {
                try
            {
                Trip trip = new Trip
                {
                    Title = _Title,
                    SupervisorId = _Supervisor.Id,
                    Date = _Date.DateTime
                };
                _trip = Newtonsoft.Json.JsonConvert.SerializeObject(trip);
                PostTrip(_trip);
                MenuNavigationHelper.UpdateView(typeof(MainPage));
            }
            catch (NullReferenceException ex)
            {
                //TODO Implement not a correct filled in form
            }
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                DateTime date = (DateTime)value;
                return new DateTimeOffset(date);
            }
            catch (Exception ex)
            {
                return DateTimeOffset.MinValue;
            }
        }


        public void PostTrip(string tripJson)
        {
            DatabaseHelper.Post(URL, tripJson);
        }

        public DateTimeOffset BindDate
        {
            get
            {
                return DateTimeOffset.Now.AddDays(1);
            }
        }

        public void testtis()
        {
            Console.WriteLine();
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
            if (data.Count != 0)
            {
                superv = data[1];
            };
        }
    }
}
