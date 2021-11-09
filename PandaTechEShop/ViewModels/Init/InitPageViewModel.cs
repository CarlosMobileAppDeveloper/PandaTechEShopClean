using System.Threading.Tasks;
using PandaTechEShop.Services;
using PandaTechEShop.Services.Token;
using PandaTechEShop.ViewModels.Base;

namespace PandaTechEShop.ViewModels.Init
{
    public class InitPageViewModel : BaseViewModel
    {
        private readonly ITokenStorageService _tokenStorageService;
        private readonly ITokenService _tokenService;
        private readonly ITokenValidatorService _tokenValidatorService;

        public InitPageViewModel(
            IBaseService baseService,
            ITokenStorageService tokenStorageService,
            ITokenService tokenService,
            ITokenValidatorService tokenValidatorService)
            : base(baseService)
        {
            _tokenStorageService = tokenStorageService;
            _tokenService = tokenService;
            _tokenValidatorService = tokenValidatorService;
        }

        public override Task OnAppearingAsync()
        {
            return StartupAsync();
        }

        private async Task StartupAsync()
        {
            // FIXME - Storing the data in keychain is cloud synced. So deleted an app, the information will still be there when the new app is installed...

            await _tokenStorageService.LoadTokenIntoMemory();

            var accessToken = _tokenService.GetAccessToken();

            if (string.IsNullOrEmpty(accessToken))
            {
                await NavigationService.NavigateAsync("/NavigationPage/SignupPage");
                return;
            }

            if (await _tokenValidatorService.HasTokenExpired())
            {
                // Update access token if expired
                if (await _tokenService.UpdateAccessToken())
                {
                    await NavigationService.NavigateAsync("/NavigationPage/HomePage");
                    return;
                }

                // Couldn't update access token, so taken them to the sign-up page
                await NavigationService.NavigateAsync("/NavigationPage/SignupPage");
                return;
            }

            // Access token is all good, take them to home page
            await NavigationService.NavigateAsync("/NavigationPage/HomePage");
            return;
        }
    }
}
