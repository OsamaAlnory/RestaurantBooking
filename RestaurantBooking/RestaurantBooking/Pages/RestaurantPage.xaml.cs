using RestaurantBooking.Database;
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
            qr.BarcodeOptions.Width = 1000;
            qr.BarcodeOptions.Height = 1000;
            for(int x = 0; x < rest.TableCount; x++)
            {
                picker.Items.Add(x+1+"");
            }
            picker.SelectedIndex = 0;
            SetQR();
            // Reload Reservations every 5 secs.
        }

        private void SetQR()
        {
            qr.BarcodeValue = rest.RestID + " " + picker.Items[picker.SelectedIndex];
        }

        // QR Show button
        private void Button_Clicked(object sender, EventArgs e)
        {
            SetQR();
        }

        // Add Menu button
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            // Open Popup with inputs

        }
    }
}