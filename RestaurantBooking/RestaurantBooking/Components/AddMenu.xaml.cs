using Plugin.Media.Abstractions;
using RestaurantBooking.Database;
using RestaurantBooking.Pages;
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
	public partial class AddMenu : StackLayout, PopupComponent
	{
        private RestaurantPage page;
        private Restaurant rest;
        private MediaFile file = null;

		public AddMenu (RestaurantPage page, Restaurant rest)
		{
            this.page = page;
            this.rest = rest;
			InitializeComponent ();
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

        private void Button_Clicked(object sender, EventArgs e)
        {
            string nm = menu_name.Text, pr = menu_price.Text, desc = menu_desc.Text;
            if(!(string.IsNullOrEmpty(nm) || string.IsNullOrEmpty(pr) || string.IsNullOrEmpty(desc)))
            {
                try
                {
                    double price = double.Parse(pr);
                    string imgId = Main.GenerateRandomImageId();
                    IImage img = new IImage { ID = imgId , Data = App.ImageToByte(file)};
                    new IMenu { MenuName = nm, Price = price, Description = desc
                    , RestID = rest.RestID, MenuImage = imgId};
                    // Save Image to Database
                    // Save Menu to Database
                }
                catch (Exception ex)
                {
                    new Popup(new ErrorMessage("Price must be a number!"), page);
                }
            } else
            {
                new Popup(new ErrorMessage("Fields cannot be empty!"), page);
            }
        }

        // Image Button
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            file = await App.OpenCamera(true);

        }
    }
}