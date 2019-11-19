using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantBooking.Components;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestaurantBooking.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage : ContentPage
	{
		public StartPage ()
		{
			InitializeComponent ();
            background.Source = App.getImage("light_blue.png");
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            new Popup(new SuccessMessage("Good"), this).Show();
        }
    }
}