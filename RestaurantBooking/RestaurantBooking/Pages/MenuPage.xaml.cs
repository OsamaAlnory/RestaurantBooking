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
    public partial class MenuPage : ContentPage
    {
        public static List<IMenuX> cart = new List<IMenuX>();
        private Restaurant restaurant;
        Random random = new Random();


        public MenuPage()
        {
            cart.Clear();
            InitializeComponent();
            AddCard("123.jpg");
            AddCard("background.jpg");
            AddCard("background.jpg");
            AddCard("background.jpg");
            AddCard("background.jpg");
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => {
                if(cart.Count > 0)
                {
                    Navigation.PushAsync(new PerformShopPage(this));
                }
            };
            cartButton.GestureRecognizers.Add(tap);
        }

        private void AddCard(string src)
        {
            box.Children.Add(new Card(src, this, new IMenu { RestID="RES123",
                MenuName ="Test"+random.Next(1000),
            Price = 20}));
        }

        public void Clicked(IMenuX menu)
        {
            cartAnimation.Play();
            cart.Add(menu);
            RefreshCount();
            if (!bubble.IsVisible)
            {
                bubble.IsVisible = true;
            }
        }

        public void RefreshCount()
        {
            bubble_count.Text = "" + cart.Count;
            if(cart.Count == 0)
            {
                bubble.IsVisible = false;
            } else
            {
                bubble.IsVisible = true;
            }
        }

        public bool IsClickAllowed()
        {
            return cart.Count < 99;
        }

        public void DeleteMenuFromID(int id)
        {
            for(int x = 0; x < cart.Count; x++)
            {
                if(cart[x].ID == id)
                {
                    cart.Remove(cart[x]);
                }
            }
        }

    }

    public class IMenuX
    {
        public int ID { get; set; }
        public IMenu MENU { get; set; }
    }

}