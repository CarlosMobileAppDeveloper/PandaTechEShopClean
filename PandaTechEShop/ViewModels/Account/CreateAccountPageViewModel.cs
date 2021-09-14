using System;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;

namespace PandaTechEShop.ViewModels.Account
{
    public class CreateAccountPageViewModel : BaseViewModel
    {
        public CreateAccountPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Welcome to Xamarin.Forms with PRISM!";
        }
    }
}
