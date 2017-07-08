using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace POAFGVApp.Views
{
    public partial class OrderDetailPage : ContentPage
    {
        public OrderDetailPage()
        {
            InitializeComponent();

            if (!App.IsFromListOrders)
                NavigationPage.SetHasNavigationBar(this, false);

            App.IsFromListOrders = false;
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}