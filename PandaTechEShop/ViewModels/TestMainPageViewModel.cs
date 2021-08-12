using System.Threading.Tasks;
using System.Windows.Input;
using PandaTechEShop.Services.Preferences;
using PandaTechEShop.ViewModels.Base;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;

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
            SaveCommand = new AsyncCommand(ExecuteSaveCommand, allowsMultipleExecutions: false);
        }

        public string Username { get; set; }

        // TODO - Make AsyncCommand using Xamarin Community Toolkit
        // TOOD - "IsProcessing" checks
        public IAsyncCommand SaveCommand { get; }

        private async Task ExecuteSaveCommand()
        {
            Username = Username?.Trim();

            if (!string.IsNullOrEmpty(Username))
            {
                _preferences.Set("Username", Username);
            }

            await Task.Delay(1);

            // await base.NavigationService.NavigateAsync("AboutPage", useModalNavigation: true);
        }
    }
}
