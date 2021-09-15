using System;
using System.Threading.Tasks;
using PandaTechEShop.Controls.Popups;
using PandaTechEShop.Services.Account;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PandaTechEShop.ViewModels.Account
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IAccountService _accountService;

        public LoginPageViewModel(INavigationService navigationService, IPopupNavigation popupNavigation, IAccountService accountService)
            : base(navigationService, popupNavigation)
        {
            Title = "Log In";
            LoginCommand = new AsyncCommand(ExecuteLoginCommandAsync, allowsMultipleExecutions: false);
            NavigateBackCommand = new AsyncCommand(ExecuteNavigateBackCommandAsync, allowsMultipleExecutions: false);
            _accountService = accountService;
        }

        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public IAsyncCommand LoginCommand { get; }
        public IAsyncCommand NavigateBackCommand { get; }

        private async Task ExecuteLoginCommandAsync()
        {
            EmailAddress?.Trim();
            Password?.Trim();
            if (!string.IsNullOrEmpty(EmailAddress) && !string.IsNullOrEmpty(Password))
            {
                var response = await _accountService.LoginAsync(EmailAddress, Password);

                if (response)
                {
                    await PopupNavigation.PushAsync(new ToastPopup("Success"));
                    await Task.Delay(500);
                    await NavigationService.NavigateAsync("/NavigationPage/HomePage");
                }
                else
                {
                    await PopupNavigation.PushAsync(new ToastPopup("Failed to login"));
                }
            }

            // await base.NavigationService.NavigateAsync("AboutPage", useModalNavigation: true);
        }

        private async Task ExecuteNavigateBackCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
