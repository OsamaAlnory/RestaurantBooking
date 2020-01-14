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
        public MenuPage()
        {
            InitializeComponent();
            AddCard("123.jpg");
            AddCard("background.jpg");
            AddCard("background.jpg");
            AddCard("background.jpg");
            AddCard("background.jpg");
        }

        private void AddCard(string src)
        {
            box.Children.Add(new Card(src, this));
        }

        public void OnTapped(Card card)
        {
            //DisplayAlert("dawd", "dawd", "dawda");
            Navigation.PushAsync(new MealDetails(card.src));
        }

    }
}