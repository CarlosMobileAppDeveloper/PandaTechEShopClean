using System;
using System.Threading.Tasks;
using PandaTechEShop.Services.Account;
using PandaTechEShop.ViewModels.Base;
using Rg.Plugins.Popup.Contracts;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;
using PandaTechEShop.Controls.Popups;

namespace PandaTechEShop.ViewModels.Account
{
    public class SignupPageViewModel : BaseViewModel
    {
        private readonly IAccountService _accountService;

        public SignupPageViewModel(INavigationService navigationService, IPopupNavigation popupNavigation, IAccountService accountService)
            : base(navigationService, popupNavigation)
        {
            Title = "Sign Up";
            SignUpCommand = new AsyncCommand(ExecuteSignUpCommandAsync, allowsMultipleExecutions: false);
            NavigateToSignInPageCommand = new AsyncCommand(ExecuteNavigateToSignInPageCommandAsync, allowsMultipleExecutions: false);
            _accountService = accountService;
        }

        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public IAsyncCommand SignUpCommand { get; }
        public IAsyncCommand NavigateToSignInPageCommand { get; }

        private async Task ExecuteSignUpCommandAsync()
        {
            Username?.Trim();
            EmailAddress?.Trim();
            Password?.Trim();
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(EmailAddress) && !string.IsNullOrEmpty(Password))
            {
                var response = await _accountService.RegisterUserAsync(Username, EmailAddress, Password);

                if (response)
                {
                    await PopupNavigation.PushAsync(new ToastPopup("Success"));
                    await Task.Delay(500);
                    await NavigationService.NavigateAsync("LoginPage", useModalNavigation: true);
                }
                else
                {
                    await PopupNavigation.PushAsync(new ToastPopup("Failed to create account for user"));
                }
            }

            // await base.NavigationService.NavigateAsync("AboutPage", useModalNavigation: true);
        }

        private async Task ExecuteNavigateToSignInPageCommandAsync()
        {
            await NavigationService.NavigateAsync("LoginPage", useModalNavigation: true);
        }
    }
}
