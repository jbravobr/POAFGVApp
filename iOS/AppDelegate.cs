using Foundation;
using UIKit;

namespace POAFGVApp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //FFImageLoading Init
            FFImageLoading.Forms.Touch.CachedImageRenderer.Init();

            global::Xamarin.Forms.Forms.Init();

            // Code for starting up the Xamarin Test Cloud Agent
#if DEBUG
			Xamarin.Calabash.Start();
#endif
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
