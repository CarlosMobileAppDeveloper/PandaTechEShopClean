using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using PandaTechEShop.Droid.Services;

namespace PandaTechEShop.Droid
{
    [Activity(Label = "PandaTechEShop", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            DependencyService.Register<DeviceOrientationLocator>();

            // LoadApplication(new App());
            LoadApplication(new App(new AndroidInitializer()));
        }

#pragma warning disable SA1202 // Elements should be ordered by access
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
#pragma warning restore SA1202 // Elements should be ordered by access
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
            }
        }
    }
}
