using System;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;

namespace PandaTechEShop.Services
{
    public interface IBaseService
    {
        INavigationService NavigationService { get; }

        ILogger Logger { get; }

        IPopupNavigation PopupNavigation { get; }
    }
}
