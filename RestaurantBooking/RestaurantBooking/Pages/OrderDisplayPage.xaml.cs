using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestaurantBooking.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrderDisplayPage : ContentPage
	{

        private bool running = false;

		public OrderDisplayPage ()
		{
			InitializeComponent ();
            orderNumber.Text = "Order Number is 2342";
            orderStatus.Text = "Status: ";
            running = true;
            Device.StartTimer(TimeSpan.FromSeconds(3), () => {
                // Reload Status
                return running;
            });
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartPage());
            App.RemovePage(this);
        }
    }
}