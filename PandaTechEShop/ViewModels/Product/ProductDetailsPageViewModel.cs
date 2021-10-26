using System;
using System.Threading.Tasks;
using PandaTechEShop.Controls.Popups;
using PandaTechEShop.Models.Product;
using PandaTechEShop.Models.ShoppingCart;
using PandaTechEShop.Services;
using PandaTechEShop.Services.Product;
using PandaTechEShop.Services.ShoppingCart;
using PandaTechEShop.Services.Token;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PandaTechEShop.ViewModels.Product
{
    public class ProductDetailsPageViewModel : BaseViewModel
    {
        private readonly ITokenService _tokenService;
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        private int _productId;

        public ProductDetailsPageViewModel(
            IBaseService baseService,
            ITokenService tokenService,
            IProductService productService,
            IShoppingCartService shoppingCartService)
            : base(baseService)
        {
            _tokenService = tokenService;
            _productService = productService;
            _shoppingCartService = shoppingCartService;
            NavigateBackCommand = new AsyncCommand(ExecuteNavigateBackCommandAsync, allowsMultipleExecutions: false);
            IncreaseQuantityCommand = new AsyncCommand(ExecuteIncreaseQuantityCommandAsync, allowsMultipleExecutions: false);
            DecreaseQuantityCommand = new AsyncCommand(ExecuteDecreaseQuantityCommandAsync, allowsMultipleExecutions: false);
            AddToCartCommand = new AsyncCommand(ExecuteAddToCartCommandAsync, allowsMultipleExecutions: false);
        }

        public ProductInfo SelectedProduct { get; set; }

        public int Quantity { get; set; } = 1;

        public IAsyncCommand IncreaseQuantityCommand { get; }

        public IAsyncCommand DecreaseQuantityCommand { get; }

        public IAsyncCommand AddToCartCommand { get; set; }

        public IAsyncCommand NavigateBackCommand { get; }

        // Load data after page is initialised and navigated to
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                if (parameters.ContainsKey("ProductId"))
                {
                    _productId = parameters.GetValue<int>("ProductId");

                    GetProductDetailsAsync().ConfigureAwait(false);
                }
            }
        }

        private async Task GetProductDetailsAsync()
        {
            SelectedProduct = await _productService.GetProductByIdAsync(_productId);
        }

        private Task ExecuteIncreaseQuantityCommandAsync()
        {
            Quantity++;
            return Task.CompletedTask;
        }

        private Task ExecuteDecreaseQuantityCommandAsync()
        {
            if (Quantity > 0)
            {
                Quantity--;
            }

            return Task.CompletedTask;
        }

        private async Task ExecuteAddToCartCommandAsync()
        {
            // TODO - Lol at price being added for one, and two that it is an integer in this AddToCart object...
            var addToCart = new AddToCart
            {
                Qty = Quantity,
                Price = Convert.ToInt32(SelectedProduct.Price),
                ProductId = SelectedProduct.Id,
                CustomerId = _tokenService.GetUserId(),
            };

            var response = await _shoppingCartService.AddItemsInCartAsync(addToCart);
            if (response)
            {
                if (Quantity > 1)
                {
                    await PopupNavigation.PushAsync(new ToastPopup("Success. Items added to cart."));
                }
                else
                {
                    await PopupNavigation.PushAsync(new ToastPopup("Success. Item added to cart."));
                }

                Quantity = 1;
            }
            else
            {
                await PopupNavigation.PushAsync(new ToastPopup("Failed to add items to cart."));
            }
        }

        private Task ExecuteNavigateBackCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }
    }
}
