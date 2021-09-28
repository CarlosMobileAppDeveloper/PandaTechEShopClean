using System;
using System.Threading.Tasks;
using PandaTechEShop.Services.Account;
using PandaTechEShop.ViewModels.Base;
using Rg.Plugins.Popup.Contracts;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;
using PandaTechEShop.Controls.Popups;
using System.Windows.Input;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.Behaviors;
using System.Linq;

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

        //public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }

        public bool IsEmailAddressValid { get; set; } = false;
        public bool IsPasswordValid { get; set; } = false;
        public bool IsPasswordMatchValid { get; set; } = false;
        public bool IsFormValid
        {
            get { return IsSignupFormValid(); }
        }

        public List<object> PasswordErrors { get; set; }
        public string PasswordError
        {
            get { return PasswordErrors?.FirstOrDefault()?.ToString(); }
        }

        public ICommand EmailValidatorCommand { get; set; }
        public ICommand PasswordValidatorCommand { get; set; }
        public ICommand PasswordMatchValidatorCommand { get; set; }
        public IAsyncCommand SignUpCommand { get; }
        public IAsyncCommand NavigateToSignInPageCommand { get; }

        private bool IsSignupFormValid()
        {
            EmailValidatorCommand.Execute(null);
            PasswordValidatorCommand.Execute(null);
            PasswordMatchValidatorCommand.Execute(null);
            return IsEmailAddressValid && IsPasswordValid && IsPasswordMatchValid;
        }

        private async Task ExecuteSignUpCommandAsync()
        {
            if(!IsFormValid)
            {
                return;
            }

            var response = await _accountService.RegisterUserAsync(string.Empty, EmailAddress, Password);

            if (response)
            {
                await PopupNavigation.PushAsync(new ToastPopup("Account successfully created."));
                await NavigationService.NavigateAsync("LoginPage", useModalNavigation: true);
                ClearForm();
            }
            else
            {
                await PopupNavigation.PushAsync(new ToastPopup("Failed to create account."));
            }
        }

        private Task ExecuteNavigateToSignInPageCommandAsync()
        {
            ClearForm();
            return NavigationService.NavigateAsync("LoginPage", useModalNavigation: true);
        }

        private void ClearForm()
        {
            EmailAddress = null;
            Password = null;
            ConfirmedPassword = null;
        }
    }
}
