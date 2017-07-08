using Xamarin.Forms;

using System.ComponentModel;

using POAFGVApp;
using POAFGVApp.Droid.Renderers;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(LineEntry), typeof(LineEntryRenderer))]
namespace POAFGVApp.Droid.Renderers
{
    public class LineEntryRenderer : EntryRenderer
    {

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (LineEntry)Element;
            if (!view.HasBorder)
                Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}