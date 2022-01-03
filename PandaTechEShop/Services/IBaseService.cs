using System;
using PandaTechEShop.Utilities.Dialog;
using PandaTechEShop.Utilities.Logger;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;

namespace PandaTechEShop.Services
{
    public interface IBaseService
    {
        INavigationService NavigationService { get; }

        ILogger Logger { get; }

        IPopupNavigation PopupNavigation { get; }
        
        IDialogService DialogService { get; }
    }
}
