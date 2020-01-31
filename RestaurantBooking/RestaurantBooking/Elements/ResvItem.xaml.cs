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
	public partial class ResvItem : Frame
	{
        private RestaurantPage page;
        private Reservation resv;
        private bool Loading;

		public ResvItem (Reservation resv, RestaurantPage page)
		{
            this.page = page;
            this.resv = resv;
			InitializeComponent ();
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => {
                menues_layout.IsVisible = !menues_layout.IsVisible;
            };
            this.GestureRecognizers.Add(tap);
            orderId.Text = resv.ID;
            orderName.Text = resv.DisplayName;
            orderTable.Text = "Table: "+resv.TableNr;
            string label = "";
            string[] d = resv.Menus.Split(';');
            foreach(string a in d)
            {
                string[] b = a.Split(',');
                label += b[1] + "x " + b[0] + "\n";
            }
            menues.Text = label;
            if(resv.Status != null && resv.Status == "Done")
            {
                button.BackgroundColor = Color.Crimson;
                button.Text = "Remove";
            } else
            {
                button.BackgroundColor = Color.LightGreen;
                button.Text = "Done";
            }
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }
            Loading = true;
            if(resv.Status == null || resv.Status != "Done")
            {
                button.BackgroundColor = Color.Crimson;
                button.Text = "Remove";
                resv = await Main.ChangeStatus(resv.ID, "Done");
            } else
            {
                await Main.RemoveResv(resv);
                page.RefreshResv();
            }
            Loading = false;
        }


    }
}