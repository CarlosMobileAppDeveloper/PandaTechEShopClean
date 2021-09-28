using System.Threading.Tasks;
using PandaTechEShop.Services.Account;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;
using PandaTechEShop.Controls.Popups;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using PandaTechEShop.Services;

namespace PandaTechEShop.ViewModels.Account
{
    public class SignupPageViewModel : BaseViewModel
    {

        private readonly IAccountService _accountService;
        private bool _hasEmailUnFocussed = false;
        private bool _hasPasswordUnFocussed = false;
        private bool _hasPasswordMatchUnFocussed = false;

        public SignupPageViewModel(IBaseService baseService, IAccountService accountService)
            : base(baseService)
        {
            Title = "Sign Up";

            _accountService = accountService;

            SignUpCommand = new AsyncCommand(SignUpAsync, allowsMultipleExecutions: false);
            NavigateToSignInPageCommand = new AsyncCommand(NavigateToSignInPageAsync, allowsMultipleExecutions: false);

            ValidateEmailCommand = new DelegateCommand(ValidateEmail);
            ForceValidateEmailCommand = new DelegateCommand(ForceValidateEmail);

            ValidatePasswordCommand = new DelegateCommand(ValidatePassword);
            ForceValidatePasswordCommand = new DelegateCommand(ForceValidatePassword);

            ValidatePasswordMatchCommand = new DelegateCommand(ValidatePasswordMatch);
            ForceValidatePasswordMatchCommand = new DelegateCommand(ForceValidatePasswordMatch);
        }

        //public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }

        public bool IsEmailAddressValid { get; set; } = true;
        public bool IsPasswordValid { get; set; } = true;
        public bool IsPasswordMatchValid { get; set; } = true;
        public bool IsFormValid
        {
            get { return IsValid(); }
        }

        public List<object> PasswordErrors { get; set; }
        public string PasswordError
        {
            get { return PasswordErrors?.FirstOrDefault()?.ToString() ?? string.Empty; }
        }

        public ICommand EmailValidatorCommand { get; set; }
        public ICommand ValidateEmailCommand { get; set; }
        public ICommand ForceValidateEmailCommand { get; set; }

        public ICommand PasswordValidatorCommand { get; set; }
        public ICommand ValidatePasswordCommand { get; set; }
        public ICommand ForceValidatePasswordCommand { get; set; }

        public ICommand PasswordMatchValidatorCommand { get; set; }
        public ICommand ValidatePasswordMatchCommand { get; set; }
        public ICommand ForceValidatePasswordMatchCommand { get; set; }

        public IAsyncCommand SignUpCommand { get; }
        public IAsyncCommand NavigateToSignInPageCommand { get; }

        private bool IsValid()
        {
            ForceValidateEmail();
            ForceValidatePassword();
            ForceValidatePasswordMatch();
            return IsEmailAddressValid && IsPasswordValid && IsPasswordMatchValid;
        }

        private async Task SignUpAsync()
        {
            if (!IsFormValid)
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

        private Task NavigateToSignInPageAsync()
        {
            ClearForm();
            return NavigationService.NavigateAsync("LoginPage", useModalNavigation: true);
        }

        private void ValidateEmail()
        {
            if (_hasEmailUnFocussed)
            {
                EmailValidatorCommand.Execute(null);
            }
        }

        private void ForceValidateEmail()
        {
            _hasEmailUnFocussed = true;
            EmailAddress?.Trim();
            EmailValidatorCommand.Execute(null);
        }

        private void ValidatePassword()
        {
            if (_hasPasswordUnFocussed)
            {
                PasswordValidatorCommand.Execute(null);
            }
        }

        private void ForceValidatePassword()
        {
            _hasPasswordUnFocussed = true;
            PasswordValidatorCommand.Execute(null);
        }

        private void ValidatePasswordMatch()
        {
            if (_hasPasswordMatchUnFocussed)
            {
                PasswordMatchValidatorCommand.Execute(null);
            }
        }

        private void ForceValidatePasswordMatch()
        {
            _hasPasswordMatchUnFocussed = true;
            PasswordMatchValidatorCommand.Execute(null);
        }

        private void ClearForm()
        {
            EmailAddress = null;
            Password = null;
            ConfirmedPassword = null;
        }
    }
}
