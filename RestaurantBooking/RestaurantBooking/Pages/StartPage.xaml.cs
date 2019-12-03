using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantBooking.Components;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace RestaurantBooking.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage : ContentPage
	{
		public StartPage ()
		{
			InitializeComponent ();
            background.Source = App.getImage("123.jpg");
            qr.Source = App.getImage("qr.png");
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) =>
            {
                Camera();
            };
            button.GestureRecognizers.Add(tap);
            
		}
          
        public async void Camera()
        {
            var camera = new ZXingScannerPage();
            camera.OnScanResult += (result) =>
            {
                camera.IsScanning = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    /*
                    string sin = "RES534432 5";
                    if (sin.StartsWith("RES"))
                    {
                        string[] d = sin.Split(' ');
                        
                    } else
                    {
                        
                    }
                    */
                    DisplayAlert("welcome", result.ToString(), "cancel");
                } );
            };
            await Navigation.PushAsync(camera);
        }

        private void Clicked(object sender, EventArgs e)
        {
            DisplayAlert("cs", "kos", "cos");
        }

    }
}