using mobile.Interfaces;
using mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace mobile.ViewModels
{
    public class TripsDetailPageModelView: ViewModelBase
    {
        private readonly ITripChildDataService _tripChildDataService;
        private ObservableCollection<TripChild> _tripChildren;
        private readonly IChildDataService _childDataService;
        private ObservableCollection<Child> _children;
        private Trip trip = new Trip();

        public TripsDetailPageModelView(INavigationService navigationService,  ITripChildDataService tripChildDataService
            , IChildDataService childDataService
            ) : base(navigationService)
        {
            _tripChildDataService = tripChildDataService;
            _childDataService = childDataService;
            TripChildren = new ObservableCollection<TripChild>();
           
            
        }

        public override async Task InitializeAsync(Object tripSelected)
        {
            Console.WriteLine();
            trip = (Trip)tripSelected;
            Console.WriteLine();
            GetTripsChildren();
        }

        public ObservableCollection<TripChild> TripChildren
        {
            get => _tripChildren;
            set
            {
                _tripChildren = value;
                OnPropertyChanged("TripChildren");
            }
        }


        private async void GetTripsChildren()
        {
            ObservableCollection<TripChild> temp = new ObservableCollection<TripChild>();
            TripChildren = (await _tripChildDataService.GetAlTripsChildren()).ToObservableCollection();
            Children = (await _childDataService.GetAllChildren()).ToObservableCollection();
            foreach(TripChild tripChild in _tripChildren)
            {
                if (tripChild.TripId == trip.Id)
                {
                    foreach (Child child in _children)
                    {
                        if (tripChild.ChildId == child.Id)
                        {
                            tripChild.Name = child.Name + " " + child.LastName;
                            temp.Add(tripChild);
                        }
                    }
                }
            }
            TripChildren = temp;
            Console.WriteLine();
        }

        public ObservableCollection<Child> Children
        {
            get => _children;
            set
            {
                _children = value;
                OnPropertyChanged();
            }
        }

        private async void GetChildren()
        {
            Children = (await _childDataService.GetAllChildren()).ToObservableCollection();
            Console.WriteLine();
        }






        public ICommand ButtonClickedCommand => new Command(ButtonClick);


        private async void ButtonClick()
        {


            var options = new MobileBarcodeScanningOptions { };

            var overlay = new ZXingDefaultOverlay
            {
                TopText = "Scan your QR code!",
                BottomText = "",
            };

            overlay.BindingContext = overlay;

            var scanPage = new ZXingScannerPage(options, overlay);

            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {

                    Scan scan = new Scan();
                    scan.Id = Convert.ToInt16(Regex.Split(result.Text, " ")[0]);
                    scan.Name = result.ToString().Substring(2);

                    overlay.BottomText = "You just scanned: " + scan.Name;
                    foreach (TripChild item in _tripChildren)
                    {
                        if (item.ChildId == scan.Id)
                        {
                            item.Scanned = true;
                            TripChildren = new ObservableCollection<TripChild>(_tripChildren);
                        }
                    }
                   
                    //GetTripsChildren();
                });
            };

            await _navigationService.NavigateToAsync(scanPage);
        }

        public ICommand ButtonClickedResetCommand => new Command(Reset);

        private void Reset()
        {
            foreach (TripChild item in _tripChildren)
            {
                item.Scanned = false;

            }
            TripChildren = new ObservableCollection<TripChild>(_tripChildren);
        }
    }
}
