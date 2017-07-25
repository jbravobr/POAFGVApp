﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Droid;
using Xamarin.Forms;
using HockeyApp.Android;

namespace POAFGVApp.Droid
{
    [Activity(Label = "PizzaBot",
              Icon = "@drawable/ic_launcher",
              Theme = "@style/MyTheme",
              MainLauncher = true,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            //Init FFImageLoading 
            CachedImageRenderer.Init();

            //Init Image Circle Plugin
            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();

            CrashManager.Register(this, "2ad8d4a715864e4bbd9bcdc81f7d1e42");
			//FeedbackManager.Register(this, "$Your_App_Id");

			//Android.Widget.Button feedbackButton = FindViewById<Android.Widget.Button>(Resource.Id.feedback_button);
			//feedbackButton.Click += delegate {
			//	FeedbackManager.ShowFeedbackActivity(ApplicationContext);
			//});

            //UserDialogs Init
            Acr.UserDialogs.UserDialogs.Init(() => (Activity)Forms.Context);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}
