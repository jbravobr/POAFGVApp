using Xamarin.Forms;

namespace POAFGVApp.Views
{
    public partial class OrderFinishedPage : ContentPage
    {
        public OrderFinishedPage()
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