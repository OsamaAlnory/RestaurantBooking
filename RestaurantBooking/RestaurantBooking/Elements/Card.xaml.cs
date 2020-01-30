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
        bool clicked = false;
        bool isLoading = true;

        public Card(MenuPage page, IMenu menu)
        {
            this.menu = menu;
            this.page = page;
            InitializeComponent();
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => {
                Clicked();
            };
            stk.GestureRecognizers.Add(tap);
            LoadImage();
        }

        private async void LoadImage()
        {
            var img = await Main.LoadImage(menu.MenuImage);
            if(img != null)
            {
                foodImg.Source = App.ByteToImage(img.Data);
            } else
            {
                foodImg.Source = App.getImage("no-image.png");
            }
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
            Main.AnimateGrowth(this, 220, "clicked");
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