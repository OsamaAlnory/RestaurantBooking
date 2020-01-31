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
        private Task action;
        private object[] o;

        public LoadingPage(string pageName, int waitTime, Task action, params object[] o)
        {
            this.action = action;
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(waitTime), () =>
            {
                Check(pageName, o);
                return false;
            });
        }

        private async void Check(string pageName, params object[] o)
        {
            loading.IsVisible = true;
            if (CrossConnectivity.Current.IsConnected)
            {
                if (action != null)
                {
                    await Task.Run(() => action);
                }
                Page page = null;
                if(pageName == "StartPage")
                {
                    page = new StartPage();
                } else if(pageName == "RestaurantPage")
                {
                    var user = o[0] as User;
                    page = new RestaurantPage(Main.GetRestaurantByUser(user), 
                        user.UType == "Display");
                } else if(pageName == "MenuPage")
                {
                    var rest = o[0] as Restaurant;
                    page = new MenuPage(rest, int.Parse(o[1].ToString()));
                }
                Navigation.PushAsync(page);
                Navigation.RemovePage(this);
            } else
            {
                loading.IsVisible = false;
                new Popup(new RetryPopup("No Network Connection!", () => {
                    Check(pageName);
                }), this).Show();
            }
        }

    }
}