using System;
namespace PandaTechEShop.Helpers
{
    public interface IDeviceOrientationServiceLocator
    {
        IDeviceOrientation Service
        {
            get;
        }
    }
}
