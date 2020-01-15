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
        private User user;

        public RestaurantPage(User loggedInAs)
        {
            this.user = loggedInAs;
            InitializeComponent();
            qr.BarcodeOptions.Width = 1000;
            qr.BarcodeOptions.Height = 1000;
            qr.BarcodeValue = Main.GenerateId()+" 10";
        }
    }
}