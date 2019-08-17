using System;
using System.Threading.Tasks;
using System.Windows.Input;
using uwp.Model;
using uwp_app.Helpers;
using uwp_app.Services;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Printing;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Printing;
using ZXing;
using ZXing.Mobile;
using ZXing.QrCode;

namespace uwp_app.Views
{
    public class ChildDetailViewModel : Observable
    {
        public string URL = "/Children";
        public int _Id { get; set; } = 0;
        public string _FirstName { get; set; } = "First name";
        public string _LastName { get; set; } = "Last name";
        public string _Class { get; set; } = "testClass";
        private string _child;
        public WriteableBitmap QRImage { get; set; }


        private PrintManager printMan;
        private PrintDocument printDoc;
        private IPrintDocumentSource printDocSource;

        public ChildDetailViewModel()
        {
           
        }

        public ICommand SaveChild => new CommandHandler(() => this.UpdateChild());
        public ICommand DeleteChildCommand => new CommandHandler(() => this.DeleteChild());
        public ICommand PrintCommand => new CommandHandler(() => this.PrintQr());


        private void UpdateChild()
        {
            Child child = new Child
            {
                Id = _Id,
                Name = _FirstName,
                LastName = _LastName,
                Class = _Class
            };
            _child = Newtonsoft.Json.JsonConvert.SerializeObject(child);
            PutChild(_child);
            MenuNavigationHelper.UpdateView(typeof(ChildDetailPage), child);
        }

        public void PutChild(string ChildJson)
        {
            DatabaseHelper.Put(URL, ChildJson);
        }

        

        private void DeleteChild()
        {
            DatabaseHelper.Delete(URL, $"{_Id}");
            MenuNavigationHelper.UpdateView(typeof(MainPage));
        }

        internal void UpdateChild(Child child)
        {
            _Id = child.Id;
            _FirstName = child.Name;
            _LastName = child.LastName;
            _Class = child.Class;   
        }

        internal void SetQR()
        {
            var options = new QrCodeEncodingOptions()
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 1000,
                Height = 1000
            };

            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            QRImage = writer.Write(_Id + " " + _FirstName +" "+ _LastName);
        }

        private async void PrintQr()
        {
            if (PrintManager.IsSupported())
            {
                try
                {
                    // Show print UI
                    await PrintManager.ShowPrintUIAsync();

                }
                catch
                {
                    // Printing cannot proceed at this time
                    ContentDialog noPrintingDialog = new ContentDialog()
                    {
                        Title = "Printing error",
                        Content = "\nSorry, printing can' t proceed at this time.",
                        PrimaryButtonText = "OK"
                    };
                    await noPrintingDialog.ShowAsync();
                }
            }
            else
            {
                // Printing is not supported on this device
                ContentDialog noPrintingDialog = new ContentDialog()
                {
                    Title = "Printing not supported",
                    Content = "\nSorry, printing is not supported on this device.",
                    PrimaryButtonText = "OK"
                };
                await noPrintingDialog.ShowAsync();
            }
        }
    }
}
