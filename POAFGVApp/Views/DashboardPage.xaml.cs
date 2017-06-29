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

        private async Task AnimMenuNewOrder()
        {
            ViewExtensions.CancelAnimations(this.btnNewNotice);
            await this.btnNewNotice.ScaleTo(3, 250, Easing.CubicIn);
            await this.btnNewNotice.ScaleTo(1, 250, Easing.CubicOut);
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
             {
                 Xamarin.Forms.MessagingCenter.Send(this, "NewOrder");
                 return true;
             });
        }

        private async Task AnimMenuOrderList()
        {
            ViewExtensions.CancelAnimations(this.btnNewNotice);
            await this.btnListNotices.ScaleTo(3, 250, Easing.CubicIn);
            await this.btnListNotices.ScaleTo(1, 250, Easing.BounceOut);
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
             {
                 Xamarin.Forms.MessagingCenter.Send(this, "OrderList");
                 return true;
             });
        }

        public DashboardPage()
        {
            InitializeComponent();
        }
    }
}