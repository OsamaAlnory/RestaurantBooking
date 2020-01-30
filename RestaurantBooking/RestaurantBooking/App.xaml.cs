using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.SharedTransitions;
using RestaurantBooking.Components;
using RestaurantBooking.Database;
using RestaurantBooking.Pages;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
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
            //MainPage = new SharedTransitionNavigationPage(new MenuPage(res, 4));
            //MainPage = new RestaurantPage(res, false);
            MainPage = new SharedTransitionNavigationPage(new LoadingPage("StartPage", 2, Main.Load()));
        }

        public static async Task<Response> SendEmail(string To, Reservation resv, double price)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("osama-alnori@outlook.com", "FeedMe");
            var subject = "Here's your reservation invoice!";
            var to = new EmailAddress(To, "Test123");
            var plainTextContent = "See your purchased items.";
            var htmlContent = "<div style=\"background-color:#fff67a;padding:10px\"><center><h1>FeedMe</h1></center>";
            var x1 = resv.Menus.Split(';');
            for(int x = 0; x < x1.Length; x++)
            {
                var x2 = x1[x].Split(',');
                htmlContent += "<p>" + x2[1] + "x "+x2[0] + "</p>";
            }
            htmlContent += "<h2>Total Price: "+price+"kr</h2>";
            htmlContent += "</div>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var bytes = await GetImageFromUriAsync("https://images.pexels.com/photos/255379/pexels-photo-255379.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500");
            //var bytes = StreamToByte("123.jpg");
            var rest = Main.GetRestaurantById(resv.RestID);
            string[] v = resv.Menus.Split(';');
            for(int x = 0; x < v.Length; x++)
            {
                var m = Main.GetMenuById(v[0]);
                var bytes = await Main.LoadImage(m.MenuImage);
                var file = Convert.ToBase64String(bytes.Data);
                msg.AddAttachment(m.MenuName+".png", file);
            }
            return await client.SendEmailAsync(msg);
        }

        public static byte[] StreamToByte(string pth)
        {
            var stream = Current.GetType().Assembly.GetManifestResourceStream(PATH+pth);
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static async Task<byte[]> GetImageFromUriAsync(string uri)
        {
            var webClient = new WebClient();
            byte[] imageBytes = await webClient.DownloadDataTaskAsync(new Uri(uri));

            return imageBytes;
        }

        public static void A(RestaurantPage page)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.sendgrid.net");
                mail.From = new MailAddress("osama-alnori@outlook.com");
                mail.To.Add("osama-alnori@outlook.com");
                mail.Subject = "Test";
                mail.Body = "Hello";
                SmtpServer.Port = 465;
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("apikey", "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");
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                new Popup(new ErrorMessage(ex.Message), page).Show();
            }
        }

        public static void OpenLoading(Page page, string pageName, int waitTime, Task action,
            params object[] o)
        {
            page.Navigation.PushAsync(new LoadingPage(pageName, waitTime, action, o));
        }

        public static void RemovePage(Page page)
        {
            page.Navigation.RemovePage(page);
        }

        public static ImageSource getImage(string loc)
        {
            return ImageSource.FromResource(PATH + loc, Assembly.GetExecutingAssembly());
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
