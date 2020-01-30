using Plugin.Media.Abstractions;
using RestaurantBooking.Database;
using RestaurantBooking.Pages;
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
	public partial class AddMenu : StackLayout, PopupComponent
	{
        private RestaurantPage page;
        private Restaurant rest;
        private MediaFile file = null;
        private bool Loading;

		public AddMenu (RestaurantPage page, Restaurant rest)
		{
            this.page = page;
            this.rest = rest;
			InitializeComponent ();
            menu_image.Source = App.getImage("no-image.png");
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => ChooseImage();
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
            if (Loading)
            {
                return;
            }
            Loading = true;
            string nm = menu_name.Text, pr = menu_price.Text, desc = menu_desc.Text;
            if(!(string.IsNullOrEmpty(nm) || string.IsNullOrEmpty(pr) || string.IsNullOrEmpty(desc)
                || file == null))
            {
                try
                {
                    double price = double.Parse(pr);
                    string imgId = Main.GenerateRandomImageId();
                    IImage img = new IImage { ID = imgId , Data = App.ImageToByte(file)};
                    await Main.AddMenu(new IMenu
                    {
                        ID = Main.GenerateRandomImageId(),
                        MenuName = nm,
                        Price = price,
                        Description = desc
                    ,
                        RestID = rest.RestID,
                        MenuImage = imgId
                    });
                    await Main.AddImage(img);
                    Navigation.PopPopupAsync();
                    new Popup(new SuccessMessage("Menu has been added!"), page).Show();
                }
                catch (Exception ex)
                {
                    new Popup(new ErrorMessage("Price must be a number!"), page).Show();
                }
            } else
            {
                new Popup(new ErrorMessage("Fields cannot be empty!"), page).Show();
            }
        }

        private async void ChooseImage()
        {
            file = await App.OpenCamera(true);
            if (file != null)
            {
                menu_image.Source = ImageSource.FromStream(() => { return file.GetStream(); });
            }
            else
            {
                menu_image.Source = App.getImage("no-image.png");
            }
        }

    }
}