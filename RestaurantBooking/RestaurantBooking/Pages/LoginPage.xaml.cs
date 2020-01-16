using RestaurantBooking.Components;
using RestaurantBooking.Database;
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string u1 = uname.Text, p1 = password.Text;
            if(!(string.IsNullOrEmpty(u1) || string.IsNullOrEmpty(p1)))
            {
                User user = User.GetUserByName(u1);
                if(user != null && user.Password == p1)
                {
                    animation.IsVisible = true;
                    animation.Play();
                    Device.StartTimer(TimeSpan.FromSeconds(4), () =>
                    {
                        Navigation.PushAsync(new RestaurantPage(user));
                        ClosePage();
                        return false;
                    });
                } else
                {
                    new Popup(new ErrorMessage("Username or password is incorrect!"), this).Show();
                }
            } else
            {
                new Popup(new ErrorMessage("Enter username and password."), this).Show();
            }
        }

        private void ClosePage()
        {
            
        }


    }
}