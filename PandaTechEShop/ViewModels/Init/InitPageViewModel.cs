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

        public InitPageViewModel(IBaseService baseService, ITokenStorageService tokenStorageService, ITokenService tokenService)
            : base(baseService)
        {
            _tokenStorageService = tokenStorageService;
            _tokenService = tokenService;
        }

        public override void OnAppearing()
        {
            StartupAsync();
        }

        public Task StartupAsync()
        {
            _tokenStorageService.LoadTokenIntoMemory();

            var accessToken = _tokenService.GetAccessToken();
            return !string.IsNullOrEmpty(accessToken)
                ? NavigationService.NavigateAsync("/NavigationPage/HomePage")
                : NavigationService.NavigateAsync("/NavigationPage/SignupPage");
        }
    }
}
