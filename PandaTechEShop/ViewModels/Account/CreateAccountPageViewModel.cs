using System;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;

namespace PandaTechEShop.ViewModels.Account
{
    public class CreateAccountPageViewModel : BaseViewModel
    {
        public CreateAccountPageViewModel(INavigationService navigationService, IPopupNavigation popupNavigation)
            : base(navigationService, popupNavigation)
        {
            Title = "Welcome to Xamarin.Forms with PRISM!";
        }
    }
}
