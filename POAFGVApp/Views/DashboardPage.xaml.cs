using System;
using System.Threading.Tasks;
using POAFGVApp.ViewModels;
using Xamarin.Forms;

namespace POAFGVApp.Views
{
    public partial class DashboardPage : ContentPage
    {
        protected override void OnAppearing()
        {
            Xamarin.Forms.MessagingCenter.Subscribe<DashboardPageViewModel>(this, "ClickMenuNewOrder", async (obj) => await AnimMenuNewOrder());
            Xamarin.Forms.MessagingCenter.Subscribe<DashboardPageViewModel>(this, "ClickMenuOrderList", async (obj) => await AnimMenuOrderList());
        }

        protected override void OnDisappearing()
        {
            Xamarin.Forms.MessagingCenter.Unsubscribe<DashboardPageViewModel>(this, "ClickMenuNewOrder");
            Xamarin.Forms.MessagingCenter.Unsubscribe<DashboardPageViewModel>(this, "ClickMenuOrderList");
        }

        private async Task AnimMenuNewOrder()
        {
            ViewExtensions.CancelAnimations(this.btnNewNotice);
            await this.btnNewNotice.ScaleTo(2, 250);
            await this.btnNewNotice.ScaleTo(1, 250);
            Device.StartTimer(TimeSpan.FromMilliseconds(2), () =>
             {
                 Xamarin.Forms.MessagingCenter.Send(this, "NewOrder", "NewOrderPage");
                 return false;
             });
        }

        private async Task AnimMenuOrderList()
        {
            ViewExtensions.CancelAnimations(this.btnNewNotice);
            await this.btnListNotices.ScaleTo(2, 250);
            await this.btnListNotices.ScaleTo(1, 250);
            Device.StartTimer(TimeSpan.FromMilliseconds(2), () =>
             {
                 Xamarin.Forms.MessagingCenter.Send(this, "OrderList", "ListOrdersPage");
                 return false;
             });
        }

        public DashboardPage()
        {
            InitializeComponent();
        }
    }
}