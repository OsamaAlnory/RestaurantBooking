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
	public partial class RestaurantDescription : StackLayout, PopupComponent
	{
		public RestaurantDescription (Restaurant rest)
		{
			InitializeComponent ();
            if (string.IsNullOrEmpty(rest.Description))
            {
                desc.IsVisible = false;
                desc1.IsVisible = false;
            }
            if (string.IsNullOrEmpty(rest.Email))
            {
                email.IsVisible = false;
                email1.IsVisible = false;
            }
            if (string.IsNullOrEmpty(rest.Tel))
            {
                tel.IsVisible = false;
                tel1.IsVisible = false;
            }
            desc.Text = rest.Description;
            email.Text = rest.Email;
            tel.Text = rest.Tel;
            label_name.Text = rest.RestName;
		}

        public bool BackgroundClose()
        {
            return true;
        }

        public PopupType GetPopupType()
        {
            return PopupType.INPUT;
        }

        public void OnClosed()
        {
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}