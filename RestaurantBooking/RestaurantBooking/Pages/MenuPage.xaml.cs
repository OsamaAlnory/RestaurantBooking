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
    public partial class MenuPage : ContentPage
    {
        public static List<IMenuX> cart = new List<IMenuX>();
        public Restaurant rest;
        public int tableNumber;
        private List<IMenu> menues = new List<IMenu>();


        public MenuPage(Restaurant rest, int tableNumber)
        {
            this.rest = rest;
            this.tableNumber = tableNumber;
            cart.Clear();
            InitializeComponent();
            Title = rest.RestName;
            for(int x = 0; x < 5; x++)
            {
                menues.Add(new IMenu { RestID = "RES123", MenuName = "Test" + App.rnd(1, 100)
                , Price = App.rnd(1, 10)*5});
            }
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) => {
                if(cart.Count > 0)
                {
                    Navigation.PushAsync(new PerformShopPage(this));
                }
            };
            cartButton.GestureRecognizers.Add(tap);
            TapGestureRecognizer tap1 = new TapGestureRecognizer();
            tap1.Tapped += (s, e) => {
                if(string.IsNullOrEmpty(rest.Description) && string.IsNullOrEmpty(rest.Email) &&
                string.IsNullOrEmpty(rest.Tel))
                {
                    return;
                }
                new Popup(new RestaurantDescription(rest), this).Show();
            };
            icon_button.GestureRecognizers.Add(tap1);
            search.SearchCommand = new Command(OnSearch);
            OnSearch();
        }

        private void OnSearch()
        {
            box.Children.Clear();
            for(int x = 0; x < menues.Count; x++)
            {
                if (string.IsNullOrEmpty(search.Text))
                {
                    AddCard(menues[x]);
                } else
                {
                    if (menues[x].MenuName.StartsWith(search.Text))
                    {
                        AddCard(menues[x]);
                    }
                }
            }
        }

        private void AddCard(IMenu m)
        {
            box.Children.Add(new Card("123.jpg", this, m));
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