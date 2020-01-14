using RestaurantBooking.Components;
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
        bool clicked = false;

        public Card(string src, MenuPage page)
        {
            this.src = src;
            this.page = page;
            InitializeComponent();
            foodImg.Source = App.getImage(src);
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => {
                page.OnTapped(this);
                Clicked();
            };
            stk.GestureRecognizers.Add(tap);
        }

        private async void Clicked()
        {
            if (clicked)
            {

                return;
            } else
            {
                clicked = true;
            }
            double a = 0;
            bool f = false;
            bool b = false;
            Device.StartTimer(TimeSpan.FromMilliseconds(20), () =>
            {
                if (!f)
                {
                    a++;
                } else
                {
                    a--;
                }
                if(a >= 5)
                {
                    f = true;
                }
                if(a <= -5)
                {
                    f = false;
                }
                this.Scale += a/220;
                if(this.Scale == 1 && b)
                {
                    clicked = false;
                    return false;
                }
                b = true;
                return true;
            });
        }

    }
}