using System;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;

namespace PandaTechEShop.Services
{
    public class BaseService : IBaseService
    {
        public BaseService(INavigationService navigationService, ILogger logger, IPopupNavigation popupNavigation)
        {
            NavigationService = navigationService;
            Logger = logger;
            PopupNavigation = popupNavigation;
        }

        public INavigationService NavigationService { get; }

        public ILogger Logger { get; }

        public IPopupNavigation PopupNavigation { get; }
    }
}
