using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using POAFGVApp.ViewModels;
using Xamarin.Forms;

namespace POAFGVApp.Views
{
    public partial class DashboardPage : ContentPage
    {
        protected override void OnAppearing()
        {
            Xamarin.Forms.MessagingCenter.Subscribe<DashboardPageViewModel>(this, "Clique", async (obj) => await Anim());
			Xamarin.Forms.MessagingCenter.Subscribe<DashboardPageViewModel>(this, "Clique2", async (obj) => await Anim2());
            }

        private async Task Anim()
        {
            ViewExtensions.CancelAnimations(this.btnNewNotice);
            await this.btnNewNotice.ScaleTo(3, 250, Easing.CubicIn);
            await this.btnNewNotice.ScaleTo(1, 250, Easing.CubicOut);
        }

		private async Task Anim2()
		{
			ViewExtensions.CancelAnimations(this.btnNewNotice);
			await this.btnListNotices.ScaleTo(3, 250, Easing.CubicIn);
			await this.btnListNotices.ScaleTo(1, 250, Easing.BounceOut);
		}

        public DashboardPage()
        {
            InitializeComponent();
        }
    }
}
