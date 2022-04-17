using System;
using System.Threading.Tasks;
using PandaTechEShop.Controls.Popups;
using PandaTechEShop.Services;
using PandaTechEShop.Services.Account;
using PandaTechEShop.ViewModels.Base;
using PandaTechEShop.Validations;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Windows.Input;
using PandaTechEShop.Constants;
using PandaTechEShop.Exceptions;
using Prism.Commands;
using XF.Material.Forms.UI.Dialogs;
using PandaTechEShop.Helpers;
using PandaTechEShop.Resources;

namespace PandaTechEShop.ViewModels.Account
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IAccountService _accountService;
        private bool _hasEmailUnFocussed = false;
        private bool _hasPasswordUnFocussed = false;
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

        public bool IsBusy { get; set; }

        private async Task LoginAsync()
        {
            if (!IsValid())
            {
                return;
            }

            //IMaterialModalPage loadingDialog = null;
            try
            {
                IsBusy = true;
                await Task.Delay(3000);
                //loadingDialog = await DialogService.ShowLoadingDialogAsync(AppResources.LoadingDialogLoggingInMessage);

                var response = await _accountService.LoginAsync(EmailAddress.Value, Password.Value);

                //await loadingDialog.DismissAsync();

                // Stop spinner before next changes
                IsBusy = false;

                if (response)
                {
                    await NavigationService.NavigateAsync($"{NavigationConstants.RootNavigationPage}/{NavigationConstants.HomePage}");
                    ClearForm();
                }
                else
                {
                    await DialogService.ShowSnackbarAsync(message: AppResources.LoginFailedUnknownErrorMessage);
                }
            }
            catch (ServiceAuthenticationException ex)
            {
                //Logger.LogWarning("ServiceAuthenticationException: " + ex.Message);
                //Console.WriteLine(ex);

                //if (loadingDialog != null)
                //{
                //    await loadingDialog.DismissAsync();
                //}

                await DialogService.ShowSnackbarAsync(
                    message: AppResources.AuthenticationFailureErrorMessage);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                //if (loadingDialog != null)
                //{
                //    await loadingDialog.DismissAsync();
                //}

                await DialogService.ShowSnackbarAsync(message: AppResources.UnkownGenericErrorMessage);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void AddValidations()
        {
            EmailAddress.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = EmailAddressResources.EmailAddressEmptyValidationErrorMessage });
            EmailAddress.Validations.Add(new EmailValidationRule<string> { ValidationMessage = EmailAddressResources.EmailAddressInvalidValidationErrorMessage });

            Password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = PasswordResources.PasswordEmptyValidationErrorMessage });
            // TODO Fix validation messages to be more dynamic
            Password.Validations.Add(new TextValidationRule<string> { ValidationMessage = PasswordResources.PasswordMinLengthValidationErrorMessage, ValidationRuleType = TextValidationRuleType.MinimumLength, MinimumLength = 8 });
        }

        private bool IsValid()
        {
            ForceValidateEmail();
            ForceValidatePassword();
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
            ForceValidateEmail();
            ForceValidatePassword();
            AddValidations();
        }
    }
}
