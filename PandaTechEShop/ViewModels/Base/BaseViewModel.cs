using System.Threading.Tasks;
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
        public BaseViewModel(INavigationService navigationService, IPopupNavigation popupNavigation)
        {
            NavigationService = navigationService;
            PopupNavigation = popupNavigation;
        }

        public string Title { get; set; }

        protected INavigationService NavigationService { get; private set; }

        public IPopupNavigation PopupNavigation { get; }

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
    }
}
