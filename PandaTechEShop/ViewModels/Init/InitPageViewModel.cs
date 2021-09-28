using System.Threading.Tasks;
using PandaTechEShop.Services;
using PandaTechEShop.Services.Preferences;
using PandaTechEShop.ViewModels.Base;

namespace PandaTechEShop.ViewModels.Init
{
    public class InitPageViewModel : BaseViewModel
    {
        private IPreferences _preferences;

        public InitPageViewModel(IBaseService baseService, IPreferences preferences)
            : base(baseService)
        {
            _preferences = preferences;
        }

        public override void OnAppearing()
        {
            StartupAsync();
        }

        public Task StartupAsync()
        {
            var accessToken = _preferences.Get("accessToken", string.Empty);
            return !string.IsNullOrEmpty(accessToken)
                ? NavigationService.NavigateAsync("/NavigationPage/HomePage")
                : NavigationService.NavigateAsync("/NavigationPage/SignupPage");
        }
    }
}
