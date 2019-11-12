using Rg.Plugins.Popup.Animations;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RestaurantBooking.Components
{
    public interface CustomPopup
    {
        void ChangeFrame(Frame frame);
        void ChangeAnimation(ScaleAnimation animation);
    }
}
