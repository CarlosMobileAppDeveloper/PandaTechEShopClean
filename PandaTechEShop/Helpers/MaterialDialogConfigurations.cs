using System;
using Xamarin.Forms;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace PandaTechEShop.Helpers
{
    public static class MaterialStylesConfigurations
    {
        public static MaterialLoadingDialogConfiguration LoadingDialogConfiguration => new MaterialLoadingDialogConfiguration()
        {
            // BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY_VARIANT),
            // MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY),
            TintColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY_VARIANT),
            // CornerRadius = 8,
            // ScrimColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY_VARIANT).MultiplyAlpha(0.32),
        };

        public static MaterialSnackbarConfiguration SnackbarConfiguration => new MaterialSnackbarConfiguration()
        {
            BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY_VARIANT),
            ButtonAllCaps = true,
            TintColor = Color.White,
            CornerRadius = 8,
            MessageTextColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.ON_PRIMARY),
        };
    }
}
