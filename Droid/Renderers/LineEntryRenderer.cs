using Xamarin.Forms;

using System.ComponentModel;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using POAFGVApp;
using POAFGVApp.Droid.Renderers;

[assembly: ExportRenderer(typeof(LineEntry), typeof(LineEntryRenderer))]
namespace POAFGVApp.Droid.Renderers
{
    public class LineEntryRenderer : EntryRenderer
    {
        private readonly Drawable originalBackground;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var view = (LineEntry)Element;

            base.OnElementPropertyChanged(sender, e);
            
                SetBorder(view);
        }

        private void SetBorder(LineEntry view)
        {
            if (view.HasBorder == false)
            {
                var shape = new ShapeDrawable(new RectShape());
                shape.Paint.Alpha = 0;
                shape.Paint.SetStyle(Paint.Style.Stroke);
                Control.SetBackgroundDrawable(shape);
            }
            else
            {
                Control.SetBackground(originalBackground);
            }
        }
    }
}