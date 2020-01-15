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
	public partial class IItem : Frame
	{
        private PerformShopPage page;
        private IMenu menu;

		public IItem (IMenu menu, int count, PerformShopPage page)
		{
            this.menu = menu;
            this.page = page;
			InitializeComponent ();
            item_info.Text = count + "x " + menu.MenuName + "    " + menu.Price + "kr";
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            page.Remove(menu);
        }
    }
}