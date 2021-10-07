using System;
using Xamarin.Forms;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace PandaTechEShop.Helpers
{
    public static class MaterialDialogConfigurations
    {
        public static MaterialLoadingDialogConfiguration LoadingDialogConfiguration => new MaterialLoadingDialogConfiguration()
        {
            BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
            MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8),
            TintColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY),
            CornerRadius = 8,
            ScrimColor = Color.FromHex("#232F34").MultiplyAlpha(0.32)
        };

        public static MaterialSnackbarConfiguration SnackbarConfiguration => new MaterialSnackbarConfiguration()
        {
            BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
            ButtonAllCaps = true,
            TintColor = Color.White,
            CornerRadius = 8,
            MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY).MultiplyAlpha(0.8)
        };
    }
}
