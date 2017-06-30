using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace POAFGVApp.Views
{
    public partial class ListOrdersPage : ContentPage
    {
        public ListOrdersPage()
        {
            InitializeComponent();

            this.lstOrders.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }
    }
}