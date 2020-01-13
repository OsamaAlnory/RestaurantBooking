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
    public partial class RestaurantPage : ContentPage
    {
        public RestaurantPage()
        {
            InitializeComponent();
            qr.BarcodeOptions.Width = 1000;
            qr.BarcodeOptions.Height = 1000;
            qr.BarcodeValue = Main.GenerateId()+" 10";
        }
    }
}