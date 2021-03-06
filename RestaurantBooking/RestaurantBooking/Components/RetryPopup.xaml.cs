﻿using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestaurantBooking.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RetryPopup : StackLayout, PopupComponent
	{
        private Action action;

		public RetryPopup (string message, Action action)
		{
            this.action = action;
			InitializeComponent ();
            lbl.Text = message;
		}

        public bool BackgroundClose()
        {
            return true;
        }

        public PopupType GetPopupType()
        {
            return PopupType.INFO;
        }

        public void OnClosed()
        {
            animation.Pause();
            action?.Invoke();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            OnClosed();
            await Navigation.PopPopupAsync();
        }

    }
}