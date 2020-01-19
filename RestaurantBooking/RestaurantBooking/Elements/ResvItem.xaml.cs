using RestaurantBooking.Database;
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
	public partial class ResvItem : StackLayout
	{

		public ResvItem (Reservation resv)
		{
			InitializeComponent ();
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => {
                menues_layout.IsVisible = !menues_layout.IsVisible;
            };
            orderId.Text = resv.ID;
            orderName.Text = resv.DisplayName;
            string label = "";
            string[] d = resv.Menues.Split(';');
            foreach(string a in d)
            {
                string[] b = a.Split(',');
                label += b[1] + "x " + b[0] + "\n";
            }
            menues.Text = label;
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            // Remove Resv
        }

    }
}