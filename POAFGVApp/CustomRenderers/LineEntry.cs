using Xamarin.Forms;

namespace POAFGVApp
{
    public class LineEntry : Entry
    {
        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create(
            nameof(HasBorder),
            typeof(bool),
            typeof(LineEntry),
            default(bool));

        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }
    }
}