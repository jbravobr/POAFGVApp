using System;
using System.Collections.Generic;
using System.Linq;
using POAFGVApp.ViewModels;
using Xamarin.Forms;

namespace POAFGVApp.Views
{
    public partial class NewOrderPage : ContentPage
    {
        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<NewOrderPageViewModel, List<BotMessage>>(this, "ScrollTo", (page, list) => ScrollListViewToEnd(list));
        }

        public NewOrderPage()
        {
            InitializeComponent();
        }

        void ScrollListViewToEnd(List<BotMessage> source)
        {
            var target = source[source.Count - 1];
            Device.BeginInvokeOnMainThread(() =>
            {
                this.MessagesListView.ScrollTo(target, ScrollToPosition.End, true);
            });
        }
    }
}