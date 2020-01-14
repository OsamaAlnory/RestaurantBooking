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
	public partial class MealDetails : ContentPage
	{
		public MealDetails ()
		{
			InitializeComponent ();
            img.Source = App.getImage("123.jpg");
		}
	}
}