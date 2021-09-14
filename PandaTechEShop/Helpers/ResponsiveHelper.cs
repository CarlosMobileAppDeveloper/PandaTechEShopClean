using System;
using Xamarin.Forms;

namespace PandaTechEShop.Helpers
{
    internal static class ResponsiveHelper
    {
        private static IDeviceOrientation _orientation;

        public static IDeviceOrientation Orientation
        {
            get
            {
                if (_orientation == null)
                {
                    IDeviceOrientationServiceLocator deviceOrientationServiceLocator = DependencyService.Get<IDeviceOrientationServiceLocator>();
                    if (deviceOrientationServiceLocator == null)
                    {
                        //return null;
                        throw new InvalidOperationException("Missing Dependency Service -IDeviceOrientationServiceLocator");
                    }
                    _orientation = deviceOrientationServiceLocator.Service;
                }
                return _orientation;
            }
        }
    }
}
