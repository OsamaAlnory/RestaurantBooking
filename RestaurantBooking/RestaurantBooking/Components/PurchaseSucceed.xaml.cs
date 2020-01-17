using RestaurantBooking.Database;
using RestaurantBooking.Pages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestaurantBooking.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseSucceed : StackLayout, PopupComponent
    {
        private Reservation res;

        public PurchaseSucceed(Reservation res)
        {
            this.res = res;
            InitializeComponent();
            orderId.Text = "Order number is "+res.ID;
            orderName.Placeholder = "Test" + App.rnd(1, 999);
        }

        public bool BackgroundClose()
        {
            return false;
        }

        public PopupType GetPopupType()
        {
            return PopupType.PURCHASE;
        }

        public void OnClosed()
        {
            animation.Pause();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            res.DisplayName = string.IsNullOrEmpty(orderName.Text) ? orderName.Text :
                orderName.Placeholder;
            Navigation.PushAsync(new OrderDisplayPage());
            /*
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("hbgtestare@outlook.com");
                mail.To.Add("osama-alnori@outlook.com");
                mail.Subject = "Test";
                mail.Body = "Hello";
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("hbgtestare@outlook.com", "hbghbg123");
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                orderId.Text = ex.Message;
                return;
            }
            */
            // Send
            OnClosed();
            await Navigation.PopPopupAsync();
        }
    }
}