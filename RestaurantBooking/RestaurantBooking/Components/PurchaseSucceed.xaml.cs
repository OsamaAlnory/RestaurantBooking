using RestaurantBooking.Database;
using RestaurantBooking.Pages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestaurantBooking.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseSucceed : StackLayout, PopupComponent
    {
        private bool Loading;
        private Reservation res;
        private double price;

        public PurchaseSucceed(Reservation res, double price)
        {
            this.res = res;
            this.price = price;
            InitializeComponent();
            orderId.Text = "Order number is "+res.ID;
            orderName.Placeholder = "Test" + App.rnd(1, 999);
        }

        public bool BackgroundClose()
        {
            return false;
        }

        public PopupType GetPopupType()
        {
            return PopupType.PURCHASE;
        }

        public void OnClosed()
        {
            animation.Pause();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }
            Loading = true;
            res.DisplayName = string.IsNullOrEmpty(orderName.Text) ? orderName.Text :
                orderName.Placeholder;
            await Main.AddResv(res);
            Navigation.PushAsync(new OrderDisplayPage(res));
            if (!string.IsNullOrEmpty(email.Text))
            {
                App.SendEmail(/*email.Text*/"osama-alnori@outlook.com", res, price);
            }
            for(int x = 0; x < Navigation.NavigationStack.Count-1; x++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[x]);
                x = -1;
            }
            OnClosed();
            await Navigation.PopPopupAsync();
        }

    }
}