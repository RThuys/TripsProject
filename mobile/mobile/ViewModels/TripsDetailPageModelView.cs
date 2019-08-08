using mobile.Interfaces;
using mobile.Models;
using System;
using System.Collections.Generic;
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
        public TripsDetailPageModelView(INavigationService navigationService
            ) : base(navigationService)
        {

        }

        public override async Task InitializeAsync(Object Trip)
        {
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
                });
            };

            await _navigationService.NavigateToAsync(scanPage);
        }
    }
}
