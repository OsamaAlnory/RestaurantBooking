using RestaurantBooking.Database;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestaurantBooking.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseSucceed : StackLayout, PopupComponent
    {
        private Reservation res;

        public PurchaseSucceed(Reservation res)
        {
            this.res = res;
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
            res.DisplayName = string.IsNullOrEmpty(orderName.Text) ? orderName.Text :
                orderName.Placeholder;
            // Send
            OnClosed();
            await Navigation.PopPopupAsync();
        }
    }
}