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
        public PurchaseSucceed()
        {
            InitializeComponent();
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
            OnClosed();
            await Navigation.PopPopupAsync();
        }
    }
}