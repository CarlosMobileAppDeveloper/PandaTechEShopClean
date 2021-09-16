using System;
using System.Threading.Tasks;
using PandaTechEShop.Controls.Popups;
using PandaTechEShop.Models.ShoppingCart;
using PandaTechEShop.Services.Preferences;
using PandaTechEShop.Services.Product;
using PandaTechEShop.Services.ShoppingCart;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PandaTechEShop.ViewModels.ShoppingCart
{
    public class ShoppingCartPageViewModel : BaseViewModel
    {
        private readonly IPreferences _preferences;
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartPageViewModel(
            INavigationService navigationService,
            IPopupNavigation popupNavigation,
            IPreferences preferences,
            IProductService productService,
            IShoppingCartService shoppingCartService)
            : base(navigationService, popupNavigation)
        {
            _preferences = preferences;
            _productService = productService;
            _shoppingCartService = shoppingCartService;
            ShoppingCartSubTotal = new CartSubTotal { SubTotal = 0 };
            ShoppingCartItems = new ObservableRangeCollection<ShoppingCartItem>();
            NavigateBackCommand = new AsyncCommand(ExecuteNavigateBackCommandAsync, allowsMultipleExecutions: false);
            ClearShoppingCartCommand = new AsyncCommand(ExecuteClearShoppingCartCommandAsync, allowsMultipleExecutions: false);
        }

        public CartSubTotal ShoppingCartSubTotal { get; set; }

        public ObservableRangeCollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public IAsyncCommand ClearShoppingCartCommand { get; }

        public IAsyncCommand NavigateBackCommand { get; }

        // Load data after page is initialised and navigated to
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                GetShoppingCartItemsAsync().ConfigureAwait(false);
                GetShoppingCartTotalAsync().ConfigureAwait(false);
            }
        }

        private async Task GetShoppingCartItemsAsync()
        {
            var cartItems = await _shoppingCartService.GetShoppingCartItemsAsync(_preferences.Get("userId", -1));
            var cartItemsCollection = new ObservableRangeCollection<ShoppingCartItem>();
            foreach (var cartItem in cartItems)
            {
                cartItemsCollection.Add(cartItem);
            }

            ShoppingCartItems = cartItemsCollection;
        }

        private async Task GetShoppingCartTotalAsync()
        {
            ShoppingCartSubTotal = await _shoppingCartService.GetShoppingCartSubTotalAsync(_preferences.Get("userId", -1));
        }

        private async Task ExecuteClearShoppingCartCommandAsync()
        {
            var response = await _shoppingCartService.ClearShoppingCartAsync(_preferences.Get("userId", -1));

            if (response)
            {
                ShoppingCartItems = new ObservableRangeCollection<ShoppingCartItem>();
                ShoppingCartSubTotal = new CartSubTotal { SubTotal = 0 };
                await PopupNavigation.PushAsync(new ToastPopup("Cart Cleared"));
            }
            else
            {
                await PopupNavigation.PushAsync(new ToastPopup("Something gone wrong. Failed to clear cart."));
            }
        }

        private Task ExecuteNavigateBackCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }
    }
}
