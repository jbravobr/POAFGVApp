using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace POAFGVApp.Views
{
    public partial class DashboardButton : ContentView
    {
        public DashboardButton()
        {
            InitializeComponent();
        }

        public ImageSource Icon
        {
            get { return DashboardButtonIcon.Source; }
            set { DashboardButtonIcon.Source = value; }
        }

        public string Label
        {
            get { return DashboardButtonLabel.Text; }
            set { DashboardButtonLabel.Text = value; }
        }
    }
}
