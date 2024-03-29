﻿using System;
using System.Threading.Tasks;
using System.Windows.Input;
using uwp.Model;
using uwp_app.Helpers;
using uwp_app.Services;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
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

        public ChildDetailViewModel()
        {
           
        }

        public ICommand SaveChild
        {
            get
            {
                return new CommandHandler(() => this.UpdateChild());
            }
        }

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

        public ICommand DeleteChildCommand
        {
            get
            {
                return new CommandHandler(() => this.DeleteChild());
            }
        }

        private void DeleteChild()
        {
            
            DeleteChild(_Id);
            MenuNavigationHelper.UpdateView(typeof(MainPage));
        }


        public void DeleteChild(int id)
        {
            DatabaseHelper.Delete(URL, $"{id}");
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
    }
}
