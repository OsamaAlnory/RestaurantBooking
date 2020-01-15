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
    public partial class Card : Frame, Taskable
    {
        static Random random = new Random();
        private IMenu menu;
        MenuPage page;
        public string src;
        bool clicked = false;
        bool isLoading = true;

        public Card(string src, MenuPage page, IMenu menu)
        {
            this.menu = menu;
            this.src = src;
            this.page = page;
            InitializeComponent();
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => {
                Clicked();
            };
            stk.GestureRecognizers.Add(tap);
            LoadImage();
        }

        private void LoadImage()
        {
            foodImg.Source = App.getImage(src);
            //
            loading_animation.Pause();
            loading_layout.IsVisible = false;
            product_layout.IsVisible = true;
            isLoading = false;
        }

        private void Clicked()
        {
            if (clicked || !page.IsClickAllowed() || isLoading)
            {
                return;
            } else
            {
                clicked = true;
            }
            page.Clicked(new IMenuX { ID = random.Next(10000), MENU = menu});
            Main.AnimateGrowth(this, 1, 1, "clicked");
        }

        public void OnRecieve(string id)
        {
            if(id == "clicked")
            {
                clicked = false;
            }
        }
    }
}