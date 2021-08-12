using PandaTechEShop.Services.Preferences;
using PandaTechEShop.ViewModels.Base;
using Prism.Commands;
using Prism.Navigation;

namespace PandaTechEShop.ViewModels
{
    public class TestMainPageViewModel : BaseViewModel
    {
        private DelegateCommand _saveCommand;
        private IPreferences _preferences;

        public TestMainPageViewModel(INavigationService navigationService, IPreferences preferences)
            : base(navigationService)
        {
            _preferences = preferences;
            Title = "Welcome to Xamarin.Forms with PRISM!";
        }

        public string Username { get; set; }

        // TODO - Make AsyncCommand using Xamarin Community Toolkit
        // TOOD - "IsProcessing" checks
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(ExecuteSaveCommand));

        private void ExecuteSaveCommand()
        {
            Username = Username?.Trim();

            if (!string.IsNullOrEmpty(Username))
            {
                _preferences.Set("Username", Username);
            }

            // await base.NavigationService.NavigateAsync("AboutPage", useModalNavigation: true);
        }
    }
}
