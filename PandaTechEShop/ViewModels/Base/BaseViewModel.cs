using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;

namespace PandaTechEShop.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public string Title { get; set; }
        protected INavigationService NavigationService { get; private set; }

        public virtual void Initialize(INavigationParameters parameters)
        {
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
    }
}
