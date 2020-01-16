using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.SharedTransitions;
using RestaurantBooking.Database;
using RestaurantBooking.Pages;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RestaurantBooking
{
    public partial class App : Application
    {

        private static string PATH = "RestaurantBooking.Images.";
        private static Random random = new Random();

        public App()
        {
            InitializeComponent();
            Restaurant res = new Restaurant { RestID = "RES1234", RestName="A7a Rest", TableCount = 14,
            Tel = "0734904433", Description = "Hello This is shit", Email = "osama@hotmail.com"};
            MainPage = new SharedTransitionNavigationPage(new MenuPage(res, 4));
            //MainPage = new NavigationPage(new LoadingPage());
            User.users.Add(new User { RestID = "fuck", Username="Test", Password = "asd", UType =1});
        }

        public static ImageSource getImage(string loc)
        {
            return ImageSource.FromResource(PATH + "" + loc, Assembly.GetExecutingAssembly());
        }

        public static byte[] ImageToByte(MediaFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                // file.Dispose();
                byte[] i = memoryStream.ToArray();
                return i;
            }
        }

        public static ImageSource ByteToImage(byte[] b)
        {
            return ImageSource.FromStream(() => new MemoryStream(b));
        }

        public static bool CheckInternetConnection()
        {
            string CheckUrl = "http://google.com";
            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);
                iNetRequest.Timeout = 8000;
                WebResponse iNetResponse = iNetRequest.GetResponse();
                iNetResponse.Close();
                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
        }

        public static async Task<MediaFile> OpenCamera(bool gallery)
        {
            MediaFile file = null;
            await CrossMedia.Current.Initialize();
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }
            if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            {
                try
                {
                    if (gallery)
                    {
                        file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { });
                    }
                    else
                    {
                        file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                        {
                            AllowCropping = true,
                            PhotoSize = PhotoSize.MaxWidthHeight
                        });
                    }
                }
                catch { }
            }
            return file;
        }

        public static int rnd(int min, int max)
        {
            return random.Next(max-min+1) + min;
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
