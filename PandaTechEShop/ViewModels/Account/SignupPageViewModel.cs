using System;
using System.Threading.Tasks;
using PandaTechEShop.Services.Account;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Windows.Input;
using PandaTechEShop.Constants;
using PandaTechEShop.Exceptions;
using PandaTechEShop.Resources;
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
            //Title = "Sign Up";

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

            var loadingDialog = await DialogService.ShowLoadingDialogAsync(message: AppResources.LoadingDialogCreatingAccountMessage);

            var response = await _accountService.RegisterUserAsync(EmailAddress.Value.ToLower(), EmailAddress.Value.ToLower(), Password.Value);

            if (response)
            {
                await LoginAsync(loadingDialog);
            }
            else
            {
                await loadingDialog.DismissAsync();

                await DialogService.ShowSnackbarAsync(message: AppResources.AccountCreationFailedErrorMessage);
            }
        }

        private async Task LoginAsync(IMaterialModalPage loadingDialog)
        {
            loadingDialog.MessageText = AppResources.LoadingDialogLoggingInMessage;

            try
            {
                var response = await _accountService.LoginAsync(EmailAddress.Value, Password.Value);

                await loadingDialog.DismissAsync();

                if (response)
                {
                    await NavigationService.NavigateAsync($"{NavigationConstants.RootNavigationPage}/{NavigationConstants.HomePage}");

                    await DialogService.ShowSnackbarAsync(message: AppResources.AccountCreatedMessage);
                }
                else
                {
                    await NavigationService.NavigateAsync($"{NavigationConstants.LoginPage}", useModalNavigation: true);

                    await DialogService.ShowSnackbarAsync(
                        message: AppResources.LoginFailedUnknownErrorMessage);
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
                    message: AppResources.AuthenticationFailureErrorMessage);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                if (loadingDialog != null)
                {
                    await loadingDialog.DismissAsync();
                }

                await DialogService.ShowSnackbarAsync(message: AppResources.UnkownGenericErrorMessage);
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
            EmailAddress.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = EmailAddressResources.EmailAddressEmptyValidationErrorMessage });
            EmailAddress.Validations.Add(new EmailValidationRule<string> { ValidationMessage = EmailAddressResources.EmailAddressInvalidValidationErrorMessage });

            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = PasswordResources.PasswordEmptyValidationErrorMessage });
            // TODO Fix validation messages to be more dynamic
            Password.Validations.Add(new TextValidationRule<string> { ValidationMessage = PasswordResources.PasswordMinLengthValidationErrorMessage, ValidationRuleType = TextValidationRuleType.MinimumLength, MinimumLength = 8 });
            Password.Validations.Add(new CharactersValidationRule<string> { ValidationMessage = PasswordResources.PasswordMinDigitCountValidationErrorMessage, CharacterType = CharacterType.Digit, MinimumCharacterCount = 1 });
            Password.Validations.Add(new CharactersValidationRule<string> { ValidationMessage = PasswordResources.PasswordMinLowercaseCountValidationErrorMessage, CharacterType = CharacterType.LowercaseLetter, MinimumCharacterCount = 1 });
            Password.Validations.Add(new CharactersValidationRule<string> { ValidationMessage = PasswordResources.PasswordMinUppercaseCountValidationErrorMessage, CharacterType = CharacterType.UppercaseLetter, MinimumCharacterCount = 1 });
            Password.Validations.Add(new CharactersValidationRule<string> { ValidationMessage = PasswordResources.PasswordMinSpecialCharacterCountValidationErrorMessage, CharacterType = CharacterType.NonAlphanumericSymbol, MinimumCharacterCount = 1 });
            Password.Validations.Add(new CharactersValidationRule<string> { ValidationMessage = PasswordResources.PasswordMinWhitespaceCountValidationErrorMessage, CharacterType = CharacterType.Whitespace, MaximumCharacterCount = 0 });

            ConfirmedPassword.Validations.Add(new RequiredStringValidationRule<string> { ValidationMessage = PasswordResources.PasswordMatchValidationErrorMessage, RequiredValidatableObject = Password });
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

            ForceValidateEmail();
            ForceValidatePassword();
            ForceValidatePasswordMatch();

            _hasEmailUnFocussed = false;
            _hasPasswordUnFocussed = false;
            _hasPasswordMatchUnFocussed = false;
            AddValidations();
        }
    }
}
