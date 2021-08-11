using System;
using PandaTechEShop.ViewModels.Base;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace PandaTechEShop.ViewModels
{
    public class TestMainPageViewModel : BaseViewModel
    {
        private DelegateCommand _navigationCommand;

        public TestMainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Welcome to Xamarin.Forms with PRISM!";
        }

        public DelegateCommand NavigateCommand => _navigationCommand ?? (_navigationCommand = new DelegateCommand(ExecuteNavigateCommand));

        private async void ExecuteNavigateCommand()
        {
            //await base.NavigationService.NavigateAsync("AboutPage", useModalNavigation: true);
        }
    }
}
