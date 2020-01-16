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
	public partial class RestaurantDescription : StackLayout, PopupComponent
	{
		public RestaurantDescription ()
		{
			InitializeComponent ();
		}

        public PopupType GetPopupType()
        {
            return PopupType.INPUT;
        }

        public void OnClosed()
        {
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}