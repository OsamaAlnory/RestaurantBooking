using Plugin.Connectivity;
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
    public partial class LoadingPage : ContentPage
    {
        private int type = 0;
        private object[] args;

        public LoadingPage(int type, params object[] args)
        {
            this.type = type;
            this.args = args;
            InitializeComponent();
            if (type == 0)
            {
                Device.StartTimer(TimeSpan.FromSeconds(2), () =>
                {
                    Check();
                    return false;
                });
            } else if(type == 1)
            {
                this.BackgroundColor = Color.Red;
                Check();
            }
        }

        private void Check()
        {
            loading.IsVisible = true;
            if (CrossConnectivity.Current.IsConnected)
            {
                if(type == 0)
                {
                    // await Load Users
                    // await Load Restaurants
                    Navigation.PushAsync(new StartPage());
                } else if(type == 1)
                {
                    // await Load Menues
                    // await Load Reservations
                    Navigation.PushAsync(new RestaurantPage(args[0] as Restaurant, 
                        args[1].ToString().ToLower() == "true"));
                }
            } else
            {
                loading.IsVisible = false;
                new Popup(new RetryPopup("No Network Connection!", () => {
                    Check();
                }), this).Show();
            }
        }

    }
}