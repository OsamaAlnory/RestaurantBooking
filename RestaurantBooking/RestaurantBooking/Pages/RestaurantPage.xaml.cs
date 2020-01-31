using RestaurantBooking.Components;
using RestaurantBooking.Database;
using RestaurantBooking.Elements;
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
    public partial class RestaurantPage : TabbedPage
    {

        private Restaurant rest;

        public RestaurantPage(Restaurant rest, bool isDisplay)
        {
            this.rest = rest;
            InitializeComponent();
            if (isDisplay)
            {
                for(int x = 1; x < Children.Count; x++)
                {
                    Children.RemoveAt(x);
                    x = 0;
                }
            }
            bkg1.Source = App.getImage("b.jpg");
            bkg2.Source = App.getImage("b.jpg");
            bkg3.Source = App.getImage("b.jpg");
            qr.BarcodeOptions.Width = 1000;
            qr.BarcodeOptions.Height = 1000;
            for(int x = 0; x < rest.TableCount; x++)
            {
                picker.Items.Add(x+1+"");
            }
            picker.SelectedIndex = 0;
            SetQR();
            RefreshMenu();
            RefreshResv();
            // Reload Reservations every 5 secs.
            Device.StartTimer(TimeSpan.FromSeconds(isDisplay?3:6), () => {
                RefreshResv();
                return true;
            });
        }

        public async void RefreshResv()
        {
            await Main.LoadResv(rest);
            layout_r.Children.Clear();
            foreach(Reservation r in Main.resv)
            {
                layout_r.Children.Add(new ResvItem(r, this));
            }
        }

        public async Task RemoveMenu(IMenu m)
        {
            await Main.RemoveMenu(m);
            RefreshMenu();
        }

        public void RefreshMenu()
        {
            layout_m.Children.Clear();
            foreach (IMenu m in Main.menu)
            {
                layout_m.Children.Add(new IMenuItem(m, this));
            }
        }

        private void SetQR()
        {
            qr.BarcodeValue = rest.RestID + " " + picker.Items[picker.SelectedIndex];
        }

        // Add Menu button
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            new Popup(new AddMenu(this, rest), this).Show();
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetQR();
        }

    }
}