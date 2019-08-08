using mobile.Interfaces;
using mobile.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace mobile.ViewModels
{
    public class TripsPageModelView : ViewModelBase
    {
        private readonly ITripDataService _tripDataService;
        private ObservableCollection<Trip> _allTrips;
        private ObservableCollection<Trip> _trips;
        private ObservableCollection<Trip> _temp;

        public TripsPageModelView(INavigationService navigationService, ITripDataService tripDataService
           ) : base(navigationService)
        {

            _tripDataService = tripDataService;
            Trips = new ObservableCollection<Trip>();
            PastTrips = new ObservableCollection<Trip>();
            GetTrips("future");
        }

        public TripsPageModelView(INavigationService navigationService) : base(navigationService) {
         }

        public ObservableCollection<Trip> Trips
        {
            get => _trips;
            set
            {
                _trips = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Trip> PastTrips
        {
            get => _trips;
            set
            {
                _trips = value;
                OnPropertyChanged();
            }
        }

        private async void GetTrips(string time)
        {
            _temp = new ObservableCollection<Trip>();
            if (time == "future")
            {
                _allTrips = (await _tripDataService.GetAllTrips()).ToObservableCollection();

                foreach (Trip trip in _allTrips)
                {

                    if (trip.Date > DateTime.Now.Date)
                    {
                        _temp.Add(trip);
                    }
                }

            }
            else
            {
                foreach (Trip trip in _allTrips)
                {

                    if (trip.Date < DateTime.Now.Date)
                    {
                        _temp.Add(trip);
                    }
                }
            }
            Trips = _temp;
            //Trips = (await _tripDataService.GetAllTrips()).ToObservableCollection();
        }

        public ICommand ViewPastTripsCommand => new Command(ViewPast);
        public ICommand ViewFutureTripsCommand => new Command(ViewFuture);

        private void ViewPast()
        {
            GetTrips("Past");
            IsFutureTripsVisible = true;
            IsPastTripsVisible = false;
        }

        private void ViewFuture()
        {
            GetTrips("future");
            IsPastTripsVisible = true;
            IsFutureTripsVisible = false;
        }

        private bool _isPastTripVisible = true;

        public bool IsPastTripsVisible
        {
            get
            {
                return _isPastTripVisible;
            }
            set
            {
                if (_isPastTripVisible != value)
                {
                    _isPastTripVisible = value;
                    OnPropertyChanged("IsPastTripsVisible");
                }
            }
        }

        private bool _isFutureTripVisible = false;

        public bool IsFutureTripsVisible
        {
            get
            {
                return _isFutureTripVisible;
            }
            set
            {
                if (_isFutureTripVisible != value)
                {
                    _isFutureTripVisible = value;
                    OnPropertyChanged("IsFutureTripsVisible");
                }
            }
        }

        private Trip _selectedTrip { get; set; }
        public Trip SelectedTrip
        {
            get { return _selectedTrip;  }
            set
            {
                if (_selectedTrip != value)
                {
                    _selectedTrip = value;
                    _navigationService.NavigateToAsync<TripsDetailPageModelView>(value);
                }
            }
        }
    }
}
