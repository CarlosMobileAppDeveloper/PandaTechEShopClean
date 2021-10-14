using System;
using System.Threading.Tasks;
using PandaTechEShop.Controls.Popups;
using PandaTechEShop.Services;
using PandaTechEShop.Services.Account;
using PandaTechEShop.ViewModels.Base;
using PandaTechEShop.Validations;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using XF.Material.Forms.UI.Dialogs;
using PandaTechEShop.Helpers;

namespace PandaTechEShop.ViewModels.Account
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IAccountService _accountService;
        private bool _hasEmailUnFocussed = false;
        private bool _hasPasswordUnFocussed = false;
        private IMaterialDialog _materialDialog;

        public LoginPageViewModel(IBaseService baseService, IAccountService accountService)
            : base(baseService)
        {
            Title = "Log In";

            _accountService = accountService;

            LoginCommand = new AsyncCommand(LoginAsync, allowsMultipleExecutions: false);
            NavigateBackCommand = new AsyncCommand(NavigateBackAsync, allowsMultipleExecutions: false);

            ValidateEmailCommand = new DelegateCommand(ValidateEmail);
            ForceValidateEmailCommand = new DelegateCommand(ForceValidateEmail);

            ValidatePasswordCommand = new DelegateCommand(ValidatePassword);
            ForceValidatePasswordCommand = new DelegateCommand(ForceValidatePassword);

            _materialDialog = MaterialDialog.Instance;

            AddValidations();
        }

        public bool IsFormValid
        {
            get { return IsValid(); }
        }

        public ValidatableObject<string> EmailAddress { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();

        public IAsyncCommand LoginCommand { get; }
        public IAsyncCommand NavigateBackCommand { get; }

        public ICommand ValidateEmailCommand { get; set; }
        public ICommand ForceValidateEmailCommand { get; set; }

        public ICommand ValidatePasswordCommand { get; set; }
        public ICommand ForceValidatePasswordCommand { get; set; }

        private async Task LoginAsync()
        {
            if (!IsValid())
            {
                return;
            }

            var loadingDialog = await _materialDialog.LoadingDialogAsync(message: "Logging In...", configuration: MaterialStylesConfigurations.LoadingDialogConfiguration);

            var response = await _accountService.LoginAsync(EmailAddress.Value, Password.Value);

            await loadingDialog.DismissAsync();

            if (response)
            {
                await NavigationService.NavigateAsync("/NavigationPage/HomePage");
                ClearForm();
            }
            else
            {
                await _materialDialog.SnackbarAsync(message: "Failed to login.", msDuration: MaterialSnackbar.DurationLong, configuration: MaterialStylesConfigurations.SnackbarConfiguration);
            }
        }

        private void AddValidations()
        {
            EmailAddress.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "An email is required." });
            EmailAddress.Validations.Add(new EmailValidationRule<string> { ValidationMessage = "Invalid email address." });

            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
            Password.Validations.Add(new TextValidationRule<string> { ValidationMessage = "Password minimum length is 8", ValidationRuleType = TextValidationRuleType.MinimumLength, MinimumLength = 8 });
        }

        private bool IsValid()
        {
            ValidateEmail();
            ForceValidateEmail();
            return EmailAddress.IsValid && Password.IsValid;
        }

        private void ValidateEmail()
        {
            if (_hasEmailUnFocussed)
            {
                EmailAddress.Validate();
            }
        }

        private void ForceValidateEmail()
        {
            _hasEmailUnFocussed = true;
            EmailAddress.Value?.Trim();
            EmailAddress.Validate();
        }

        private void ValidatePassword()
        {
            if (_hasPasswordUnFocussed)
            {
                Password.Validate();
            }
        }

        private void ForceValidatePassword()
        {
            _hasPasswordUnFocussed = true;
            Password.Validate();
        }

        private void ClearForm()
        {
            EmailAddress = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            AddValidations();
        }
    }
}
