using Plugin.Connectivity;
using RestaurantBooking.Components;
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
        public LoadingPage()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(2), () => {
                Check();
                return false;
            });
        }

        private void Check()
        {
            loading.IsVisible = true;
            if (CrossConnectivity.Current.IsConnected)
            {
                Navigation.PushAsync(new StartPage());
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