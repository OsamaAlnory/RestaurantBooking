using RestaurantBooking.Database;
using RestaurantBooking.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestaurantBooking.Elements
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IMenuItem : StackLayout
	{
        private RestaurantPage page;
        private IMenu menu;
        private bool Loading;

		public IMenuItem (IMenu menu, RestaurantPage page)
		{
            this.menu = menu;
            this.page = page;
			InitializeComponent ();
            menu_name.Text = menu.MenuName;
            menu_price.Text = menu.Price+"kr";
            icon.Source = App.getImage("qr.png");
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }
            Loading = true;
            await page.RemoveMenu(menu);
            // Loading = false;
        }

    }
}