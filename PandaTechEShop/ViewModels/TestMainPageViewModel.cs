using System;
using PandaTechEShop.Services.Preferences;
using PandaTechEShop.ViewModels.Base;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace PandaTechEShop.ViewModels
{
    public class TestMainPageViewModel : BaseViewModel
    {
        private DelegateCommand _saveCommand;
        private IPreferences _preferences;

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        public TestMainPageViewModel(INavigationService navigationService, IPreferences preferences)
            : base(navigationService)
        {
            _preferences = preferences;
            Title = "Welcome to Xamarin.Forms with PRISM!";
        }

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(ExecuteSaveCommand));

        private void ExecuteSaveCommand()
        {
            Username = Username?.Trim();

            if(!string.IsNullOrEmpty(_username))
            {
                _preferences.Set("Username", _username);
            }
            //await base.NavigationService.NavigateAsync("AboutPage", useModalNavigation: true);
        }
    }
}
