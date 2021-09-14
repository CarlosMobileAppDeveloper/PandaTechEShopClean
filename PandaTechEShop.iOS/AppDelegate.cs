using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using PandaTechEShop.iOS.Services;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace PandaTechEShop.iOS
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            DependencyService.Register<DeviceOrientationLocator>();

            LoadApplication(new App(new iOSInitializer()));
            return base.FinishedLaunching(app, options);
        }
    }

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable SA1402 // File may only contain a single type
    public class iOSInitializer : IPlatformInitializer
#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore IDE1006 // Naming Styles
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
