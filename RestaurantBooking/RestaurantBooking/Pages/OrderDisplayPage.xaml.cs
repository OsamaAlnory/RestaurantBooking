using RestaurantBooking.Components;
using RestaurantBooking.Database;
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
	public partial class OrderDisplayPage : ContentPage, Taskable
	{
        private string id;
        private Reservation resv;
        private bool running = true;

		public OrderDisplayPage (Reservation resv)
		{
            this.resv = resv;
            this.id = resv.ID;
			InitializeComponent ();
            orderNumber.Text = "Order Number is " + resv.ID;
            orderStatus.Text = "Waiting";
            Device.StartTimer(TimeSpan.FromSeconds(3), () => {
                ReloadStatus();
                return running;
            });
		}

        private async void ReloadStatus()
        {
            resv = await Main.GetResv(id);
            if(resv != null)
            {
                if(resv.Status == "Done")
                {
                    Ready();
                }
            } else
            {
                running = false;
            }
        }

        private void Ready()
        {
            orderStatus.Text = "Ready!";
            Main.AnimateColorTo(this, 5, "color", Color.FromHex("#34c23b"));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartPage());
            App.RemovePage(this);
        }

        public void OnRecieve(string id)
        {
            if(id == "color")
            {

            }
        }

    }
}