using System;
using System.Threading.Tasks;
using PandaTechEShop.Services.Account;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Windows.Input;
using PandaTechEShop.Constants;
using PandaTechEShop.Exceptions;
using Prism.Commands;
using PandaTechEShop.Services;
using XF.Material.Forms.UI.Dialogs;
using PandaTechEShop.Validations;

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

            AddValidations();
        }

        public bool IsFormValid
        {
            get { return IsValid(); }
        }

        //public string Username { get; set; }
        public IValidatableObject<string> EmailAddress { get; set; } = new ValidatableObject<string>();
        public IValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();
        public IValidatableObject<string> ConfirmedPassword { get; set; } = new ValidatableObject<string>();

        public ICommand ValidateEmailCommand { get; set; }
        public ICommand ForceValidateEmailCommand { get; set; }

        public ICommand ValidatePasswordCommand { get; set; }
        public ICommand ForceValidatePasswordCommand { get; set; }

        public ICommand ValidatePasswordMatchCommand { get; set; }
        public ICommand ForceValidatePasswordMatchCommand { get; set; }

        public IAsyncCommand SignUpCommand { get; }
        public IAsyncCommand NavigateToSignInPageCommand { get; }

        private async Task SignUpAsync()
        {
            if (!IsValid())
            {
                return;
            }

            var loadingDialog = await DialogService.ShowLoadingDialogAsync(message: "Creating Account...");

            var response = await _accountService.RegisterUserAsync(string.Empty, EmailAddress.Value, Password.Value);

            if (response)
            {
                await LoginAsync(loadingDialog);
            }
            else
            {
                await loadingDialog.DismissAsync();

                await DialogService.ShowSnackbarAsync(
                    message: "Something went wrong. Failed to create account. Please try again.");
            }
        }

        private async Task LoginAsync(IMaterialModalPage loadingDialog)
        {
            loadingDialog.MessageText = "Logging In...";

            try
            {
                var response = await _accountService.LoginAsync(EmailAddress.Value, Password.Value);

                await loadingDialog.DismissAsync();

                if (response)
                {
                    await NavigationService.NavigateAsync($"{NavigationConstants.RootNavigationPage}/{NavigationConstants.HomePage}");

                    await DialogService.ShowSnackbarAsync(message: "Account successfully created.");
                }
                else
                {
                    await NavigationService.NavigateAsync($"{NavigationConstants.LoginPage}", useModalNavigation: true);

                    await DialogService.ShowSnackbarAsync(
                        message: "Something went wrong. Failed to login. Please try again.");
                }
            }
            catch (ServiceAuthenticationException ex)
            {
                //Logger.LogWarning("ServiceAuthenticationException: " + ex.Message);
                //Console.WriteLine(ex);
                if (loadingDialog != null)
                {
                    await loadingDialog.DismissAsync();
                }

                await DialogService.ShowSnackbarAsync(
                    message: "Authentication failed. Please check your username and password.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                if (loadingDialog != null)
                {
                    await loadingDialog.DismissAsync();
                }

                await DialogService.ShowSnackbarAsync(message: "Something went wrong. Please try again later.");
            }
            finally
            {
                ResetForm();
            }
        }

        private Task NavigateToSignInPageAsync()
        {
            ResetForm();
            return NavigationService.NavigateAsync($"{NavigationConstants.LoginPage}", useModalNavigation: true);
        }

        private void AddValidations()
        {
            EmailAddress.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "An email is required." });
            EmailAddress.Validations.Add(new EmailValidationRule<string> { ValidationMessage = "Invalid email address." });

            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A password is required." });
            Password.Validations.Add(new TextValidationRule<string> { ValidationMessage = "Password minimum length is 8", ValidationRuleType = TextValidationRuleType.MinimumLength, MinimumLength = 8 });
            Password.Validations.Add(new CharactersValidationRule<string> { ValidationMessage = "Password must have 1 digit", CharacterType = CharacterType.Digit, MinimumCharacterCount = 1 });
            Password.Validations.Add(new CharactersValidationRule<string> { ValidationMessage = "Password must have 1 lowercase character", CharacterType = CharacterType.LowercaseLetter, MinimumCharacterCount = 1 });
            Password.Validations.Add(new CharactersValidationRule<string> { ValidationMessage = "Password must have 1 uppercase character", CharacterType = CharacterType.UppercaseLetter, MinimumCharacterCount = 1 });
            Password.Validations.Add(new CharactersValidationRule<string> { ValidationMessage = "Password must have 1 special character", CharacterType = CharacterType.NonAlphanumericSymbol, MinimumCharacterCount = 1 });
            Password.Validations.Add(new CharactersValidationRule<string> { ValidationMessage = "Password cannot have any spaces", CharacterType = CharacterType.Whitespace, MaximumCharacterCount = 0 });

            ConfirmedPassword.Validations.Add(new RequiredStringValidationRule<string> { ValidationMessage = "Password and confirm password must match", RequiredValidatableObject = Password });
        }

        private bool IsValid()
        {
            ForceValidateEmail();
            ForceValidatePassword();
            ForceValidatePasswordMatch();
            return EmailAddress.IsValid && Password.IsValid & ConfirmedPassword.IsValid;
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

        private void ValidatePasswordMatch()
        {
            if (_hasPasswordMatchUnFocussed)
            {
                ConfirmedPassword.Validate();
            }
        }

        private void ForceValidatePasswordMatch()
        {
            _hasPasswordMatchUnFocussed = true;
            ConfirmedPassword.Validate();
        }

        private void ResetForm()
        {
            EmailAddress = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            ConfirmedPassword = new ValidatableObject<string>();
            _hasEmailUnFocussed = false;
            _hasPasswordUnFocussed = false;
            _hasPasswordMatchUnFocussed = false;
            AddValidations();
        }
    }
}
