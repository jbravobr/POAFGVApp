﻿using Xamarin.Forms;

namespace POAFGVApp.Views
{
    public partial class ConfirmOrderPage : ContentPage
    {
        public ConfirmOrderPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}