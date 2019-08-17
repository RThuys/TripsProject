using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using uwp.Model;
using uwp_app.Helpers;
using uwp_app.Model;
using uwp_app.Services;
using uwp_app.Views;
using Windows.UI.Xaml.Controls;

namespace uwp_app.ViewModels
{
    public class TripDetailViewModel : Observable
    {
        
        public string URL = "/Trips";
        public int _Id { get; set; }
        public string _Title { get; set; }
        public string _Date { get; set; }
        public Trip trip;

        private string _tripChild;


        public TripDetailViewModel()
        {

    }

        internal void InitializeTripAsync(Trip trip)
        {
            this.trip = trip;
            _Id = trip.Id;
            _Title = trip.Title;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
            _Date = trip.Date.ToShortDateString();

            GetSupervisor(trip);
            GetAllTripsChildren();
            GetAllChildren();
            GetAllAddedChildren();

            
        }

        public ICommand DeleteTripCommand => new CommandHandler(() => this.DeleteTrip());

        private async void DeleteTrip()
        {
            ContentDialog noPrintingDialog = new ContentDialog()
            {
                Title = "Not Implemented error",
                Content = "\nSorry, We have not yet implemented this feature on purpose.",
                PrimaryButtonText = "OK"
            };
            await noPrintingDialog.ShowAsync();

        }

        public ICommand UpdateTripCommand => new CommandHandler(() => this.UpdateTrip());

        private void UpdateTrip()
        {
            if (_Childs != null)
            {
                foreach (Child child in _Childs)
                {
                    bool found = false;
                    TripChild tripChild = new TripChild
                    {
                        TripId = _Id,
                        ChildId = child.Id,
                        Scanned = false
                    };
                    foreach(TripChild trip in TripsChildren)
                    {
                        if (trip.TripId == _Id && trip.ChildId == child.Id)
                        {
                            found = true;
                        }
                    }
                    if (found == false)
                    {
                        _tripChild = Newtonsoft.Json.JsonConvert.SerializeObject(tripChild);
                        DatabaseHelper.Post("/TripsChildren", _tripChild);
                    }
                }
                MenuNavigationHelper.UpdateView(typeof(TripsOverviewPage));
            }
            //await Task.Delay(100);
            //GetAllAddedChildren();
           
        }

        private List<TripChild> _tripsChildren;
        public List<TripChild> TripsChildren
        {
            get { return _tripsChildren; }
            set { Set(ref _tripsChildren, value, "TripsChildren"); }

        }

        private async void GetAllTripsChildren()
        {
            string response = await DatabaseHelper.CreateClient("/TripsChildren");
            var data = JsonConvert.DeserializeObject<List<TripChild>>(response);

            TripsChildren = data;
        }

        private Supervisor _supervisor;
        public Supervisor Supervisor
        {
            get { return _supervisor; }
            set { Set(ref _supervisor, value, "Supervisor"); }

        }

        private async void  GetSupervisor(Trip trip)
        {
            string response = await DatabaseHelper.CreateClient("/Supervisors");
            var data = JsonConvert.DeserializeObject<List<Supervisor>>(response);
            Supervisor = data[trip.SupervisorId - 1];
        }

        private List<Child> _children;
        public List<Child> Children
        {
            get { return _children; }
            set { Set(ref _children, value, "Children"); }

        }

        private async void GetAllChildren()
        {
            string response = await DatabaseHelper.CreateClient("/TripsChildren/Trip/" + _Id + "/ChildrenNot");
            var data = JsonConvert.DeserializeObject<List<Child>>(response);

           
            Children = data;
        }

        public List<Child> _Childs;

        public void ClickItemList(object sender, ItemClickEventArgs e)
        {
            Child clickedItem = (Child)e.ClickedItem;
            if (_Childs == null)
            {
                _Childs = new List<Child>();
                _Childs.Add(clickedItem);
            } else if ( _Childs.Contains(clickedItem))
            {
                _Childs.Remove(clickedItem);
            } else
            {
                _Childs.Add(clickedItem);
            }
            //MenuNavigationHelper.UpdateView(typeof(ChildDetailPage), clickedItem);
        }

        private List<Child> _childrenAdded;
        public List<Child> ChildrenAdded
        {
            get { return _childrenAdded; }
            set { Set(ref _childrenAdded, value, "ChildrenAdded"); }

        }

        private async void GetAllAddedChildren()
        {
            string response = await DatabaseHelper.CreateClient("/TripsChildren/Trip/" + _Id + "/Children");
            var data = JsonConvert.DeserializeObject<List<Child>>(response);
           
            ChildrenAdded = data;
        }

        public void ClickedChild(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (Child)e.ClickedItem;
            MenuNavigationHelper.UpdateView(typeof(ChildDetailPage), clickedItem);
        }


        private async void temp()
        {
            string response = await DatabaseHelper.CreateClient("/TripsChildren");
            var data = JsonConvert.DeserializeObject<List<TripChild>>(response);
            List<Child> data2 = new List<Child>();

            foreach (var temp in data)
            {
                if (temp.TripId == _Id && Children != null)
                {
                    foreach (Child child in Children)
                    {
                        if (temp.ChildId == child.Id)
                        {
                            data2.Add(child);
                        }
                    }

                }
            }
            ChildrenAdded = data2;
        }
    }
}
