using System;
using PandaTechEShop.Enums;

namespace PandaTechEShop.Helpers
{
    public interface IDeviceOrientation
    {
        DeviceOrientation Orientation
        {
            get;
        }

        bool IsPortrait
        {
            get;
        }

        bool IsLandscape
        {
            get;
        }

        event EventHandler OrientationChanged;
    }
}
