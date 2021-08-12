using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace PandaTechEShop.iOS
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
    public class Application
    {
        // This is the main entry point of the application.
#pragma warning disable SA1400 // Access modifier should be declared
        static void Main(string[] args)
#pragma warning restore SA1400 // Access modifier should be declared
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
