using System;
using System.Threading.Tasks;
using PandaTechEShop.Services.Account;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PandaTechEShop.ViewModels.Account
{
    public class SignupPageViewModel : BaseViewModel
    {
        private readonly IAccountService _accountService;

        public SignupPageViewModel(INavigationService navigationService, IAccountService accountService)
            : base(navigationService)
        {
            Title = "Sign Up";
            SignUpCommand = new AsyncCommand(ExecuteSignUpCommandAsync, allowsMultipleExecutions: false);
            _accountService = accountService;
        }

        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public IAsyncCommand SignUpCommand { get; }

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

                }
            }

            // await base.NavigationService.NavigateAsync("AboutPage", useModalNavigation: true);
        }
    }
}
