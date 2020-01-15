using RestaurantBooking.Components;
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
    public partial class Card : Frame
    {
        static Random random = new Random();
        private IMenu menu;
        MenuPage page;
        public string src;
        bool clicked = false;

        public Card(string src, MenuPage page, IMenu menu)
        {
            this.menu = menu;
            this.src = src;
            this.page = page;
            InitializeComponent();
            foodImg.Source = App.getImage(src);
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => {
                Clicked();
            };
            stk.GestureRecognizers.Add(tap);
        }

        private void Clicked()
        {
            if (clicked || !page.IsClickAllowed())
            {
                return;
            } else
            {
                clicked = true;
            }
            page.Clicked(new IMenuX { ID = random.Next(10000), MENU = menu});
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