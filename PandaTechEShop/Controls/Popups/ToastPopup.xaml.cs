using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.ViewModels.Popups;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace PandaTechEShop.Controls.Popups
{
    public partial class ToastPopup : PopupPage
    {
        public ToastPopup(string message)
        {
            InitializeComponent();

            BindingContext = new ToastPopupViewModel(message);
        }

        public async Task Hide(int millisecondsDuration = 2000)
        {
            _ = await Task.Delay(millisecondsDuration)
                .ContinueWith(async _ => await PopupNavigation.Instance.PopAsync());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Hide().ConfigureAwait(false);
        }
    }
}
