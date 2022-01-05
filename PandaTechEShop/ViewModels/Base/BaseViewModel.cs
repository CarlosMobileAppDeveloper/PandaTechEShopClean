using System.Threading.Tasks;
using PandaTechEShop.Services;
using PandaTechEShop.Utilities.Dialog;
using PandaTechEShop.Utilities.Logger;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;
using Rg.Plugins.Popup.Contracts;

namespace PandaTechEShop.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware, IDestructible, IPageLifecycleAware
    {
        public BaseViewModel(IBaseService baseService)
        {
            NavigationService = baseService.NavigationService;
            Logger = baseService.Logger;
            PopupNavigation = baseService.PopupNavigation;
            DialogService = baseService.DialogService;
        }

        public string Title { get; set; }

        protected INavigationService NavigationService { get; private set; }
        protected ILogger Logger { get; private set; }
        protected IPopupNavigation PopupNavigation { get; private set; }
        protected IDialogService DialogService { get; }

        public virtual void Initialize(INavigationParameters parameters)
        {
            InitializeAsync(parameters).ConfigureAwait(false);
        }

        public virtual Task InitializeAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void Destroy()
        {
        }

        public virtual void OnAppearing()
        {
            OnAppearingAsync().ConfigureAwait(false);
        }

        public virtual Task OnAppearingAsync()
        {
            return Task.CompletedTask;
        }

        public virtual void OnDisappearing()
        {
        }

        public virtual async Task NavigateBackAsync()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
