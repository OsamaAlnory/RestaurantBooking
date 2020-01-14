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
    public partial class Card : Frame
    {
        MenuPage page;
        public string src;

        public Card(string src, MenuPage page)
        {
            this.src = src;
            this.page = page;
            InitializeComponent();
            foodImg.Source = App.getImage(src);
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => {
                page.OnTapped(this);
                //
            };
            stk.GestureRecognizers.Add(tap);
        }
    }
}