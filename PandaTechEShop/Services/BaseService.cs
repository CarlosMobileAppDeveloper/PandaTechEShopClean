using System;
using PandaTechEShop.Utilities.Dialog;
using PandaTechEShop.Utilities.Logger;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;

namespace PandaTechEShop.Services
{
    public class BaseService : IBaseService
    {
        public BaseService(INavigationService navigationService, ILogger logger, IPopupNavigation popupNavigation, IDialogService dialogService)
        {
            NavigationService = navigationService;
            Logger = logger;
            PopupNavigation = popupNavigation;
            DialogService = dialogService;
        }

        public INavigationService NavigationService { get; }
        public ILogger Logger { get; }
        public IPopupNavigation PopupNavigation { get; }
        public IDialogService DialogService { get; }
    }
}
