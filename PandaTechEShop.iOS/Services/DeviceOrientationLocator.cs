using System;
using PandaTechEShop.Helpers;

namespace PandaTechEShop.iOS.Services
{
    internal class DeviceOrientationLocator : IDeviceOrientationServiceLocator
    {
        public IDeviceOrientation Service => DeviceOrientationImpl.Instance;
    }
}
