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
            qr1.Source = App.getImage("qrcode.png");
            icon.Source = App.getImage("main-icon.png");
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
                    string res = result.ToString();
                    // "RES123 10"
                    string[] d = res.Split(' ');
                    DisplayAlert("welcome", "Table number is "+d[1]+ " ks", "cancel");
                    //Navigation.PushAsync(new MenuPage());
                } );
            };
            await Navigation.PushAsync(camera);
        }

        private void Clicked(object sender, EventArgs e)
        {
            DisplayAlert("cs", "kos", "cos");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}