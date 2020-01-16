using Plugin.SharedTransitions;
using RestaurantBooking.Database;
using RestaurantBooking.Pages;
using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RestaurantBooking
{
    public partial class App : Application
    {

        private static string PATH = "RestaurantBooking.Images.";

        public App()
        {
            InitializeComponent();
            Restaurant res = new Restaurant { RestID = "RES1234", RestName="A7a Rest", TableCount = 14,
            Tel = "0734904433", Description = "Hello This is shit", Email = "osama@hotmail.com"};
            MainPage = new SharedTransitionNavigationPage(new MenuPage(res));
            //MainPage = new NavigationPage(new StartPage());
            User.users.Add(new User { RestID = "fuck", Username="Test", Password = "asd", UType =1});
        }

        public static ImageSource getImage(string loc)
        {
            return ImageSource.FromResource(PATH + "" + loc, Assembly.GetExecutingAssembly());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
