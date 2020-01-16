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
    public partial class PerformShopPage : ContentPage
    {
        private Dictionary<IMenu, int> menues = new Dictionary<IMenu, int>();
        private MenuPage page;

        public PerformShopPage(MenuPage page)
        {
            this.page = page;
            InitializeComponent();
            background_image.Source = App.getImage("bkg.jpg");
            table_label.Text = "Table Number is " + page.rest.TableCount;
            Reload();
            // Meal Status
        }

        public void Remove(IMenu menu)
        {
            for(int x = 0; x < MenuPage.cart.Count; x++)
            {
                IMenuX m = MenuPage.cart[x];
                if(m.MENU.MenuName == menu.MenuName)
                {
                    MenuPage.cart.Remove(m);
                    x = -1;
                }
            }
            Reload();
            page.RefreshCount();
        }

        public void Reload()
        {
            layout.Children.Clear();
            menues.Clear();
            double total = 0;
            for(int x = 0; x < MenuPage.cart.Count; x++)
            {
                IMenuX menu = MenuPage.cart[x];
                if (menues.ContainsKey(menu.MENU))
                {
                    menues[menu.MENU]++;
                } else
                {
                    menues.Add(menu.MENU, 1);
                }
                total += menu.MENU.Price;
            }
            foreach(IMenu menu in menues.Keys)
            {
                layout.Children.Add(new IItem(menu, menues[menu], this));
            }
            total_label.Text = "To pay: "+total+"kr";
            if(total <= 0)
            {
                button.IsEnabled = false;
            } else
            {
                button.IsEnabled = true;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            // On Purchase
            string toString = "";
            int x = 0;
            foreach(IMenu menu in menues.Keys)
            {
                if(x > 0)
                {
                    toString += ";";
                }
                toString += menu.MenuName + "," + menues[menu];
                x++;
            }
            Reservation res = new Reservation { Menues = toString };
            new Popup(new PurchaseSucceed(), this).Show();
        }

    }
}