using System.Threading.Tasks;
using PandaTechEShop.Constants;
using PandaTechEShop.Controls.Popups;
using PandaTechEShop.Models.ShoppingCart;
using PandaTechEShop.Services;
using PandaTechEShop.Services.ShoppingCart;
using PandaTechEShop.Services.Token;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PandaTechEShop.ViewModels.ShoppingCart
{
    public class ShoppingCartPageViewModel : BaseViewModel
    {
        private readonly ITokenService _tokenService;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartPageViewModel(
            IBaseService baseService,
            ITokenService tokenService,
            IShoppingCartService shoppingCartService)
            : base(baseService)
        {
            _tokenService = tokenService;
            _shoppingCartService = shoppingCartService;
            ShoppingCartSubTotal = new CartSubTotal { SubTotal = 0 };
            ShoppingCartItems = new ObservableRangeCollection<ShoppingCartItem>();
            NavigateBackCommand = new AsyncCommand(ExecuteNavigateBackCommandAsync, allowsMultipleExecutions: false);
            ClearShoppingCartCommand = new AsyncCommand(ExecuteClearShoppingCartCommandAsync, allowsMultipleExecutions: false);
            ProceedWithOrderCommand = new AsyncCommand(ExecuteProceedWithOrderCommandAsync, allowsMultipleExecutions: false);
        }

        public CartSubTotal ShoppingCartSubTotal { get; set; }

        public ObservableRangeCollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public IAsyncCommand ClearShoppingCartCommand { get; }

        public IAsyncCommand ProceedWithOrderCommand { get; }

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
            var cartItems = await _shoppingCartService.GetShoppingCartItemsAsync(_tokenService.GetUserId());
            var cartItemsCollection = new ObservableRangeCollection<ShoppingCartItem>();
            foreach (var cartItem in cartItems)
            {
                cartItemsCollection.Add(cartItem);
            }

            ShoppingCartItems = cartItemsCollection;
        }

        private async Task GetShoppingCartTotalAsync()
        {
            ShoppingCartSubTotal = await _shoppingCartService.GetShoppingCartSubTotalAsync(_tokenService.GetUserId());
        }

        private async Task ExecuteClearShoppingCartCommandAsync()
        {
            var response = await _shoppingCartService.ClearShoppingCartAsync(_tokenService.GetUserId());

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

        private Task ExecuteProceedWithOrderCommandAsync()
        {
            var parameters = new NavigationParameters
            {
                { "OrderTotal", ShoppingCartSubTotal.SubTotal },
            };

            return NavigationService.NavigateAsync($"{NavigationConstants.PlaceOrderPage}", parameters);
        }

        private Task ExecuteNavigateBackCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }
    }
}
