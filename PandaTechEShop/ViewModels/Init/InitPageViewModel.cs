using System;
using System.Threading.Tasks;
using PandaTechEShop.Services.Preferences;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;

namespace PandaTechEShop.ViewModels.Init
{
    public class InitPageViewModel : BaseViewModel
    {
        private IPreferences _preferences;

        public InitPageViewModel(INavigationService navigationService, IPopupNavigation popupNavigation, IPreferences preferences)
            : base(navigationService, popupNavigation)
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
