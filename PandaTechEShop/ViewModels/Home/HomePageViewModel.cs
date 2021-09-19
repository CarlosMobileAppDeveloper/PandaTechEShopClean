using System;
using System.Threading.Tasks;
using PandaTechEShop.Controls.Popups;
using PandaTechEShop.Models.Category;
using PandaTechEShop.Models.Product;
using PandaTechEShop.Services.Category;
using PandaTechEShop.Services.Preferences;
using PandaTechEShop.Services.Product;
using PandaTechEShop.Services.ShoppingCart;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PandaTechEShop.ViewModels.Home
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly IPreferences _preferences;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IShoppingCartService _shoppingCartService;

        public HomePageViewModel(
            INavigationService navigationService,
            IPopupNavigation popupNavigation,
            IPreferences preferences,
            IProductService productService,
            ICategoryService categoryService,
            IShoppingCartService shoppingCartService)
            : base(navigationService, popupNavigation)
        {
            Title = "Panda eShop";

            _preferences = preferences;
            _productService = productService;
            _categoryService = categoryService;
            _shoppingCartService = shoppingCartService;

            TrendingProducts = new ObservableRangeCollection<TrendingProduct>();
            Categories = new ObservableRangeCollection<CategoryInfo>();

            ViewProductForCategoryCommand = new AsyncCommand(ExecuteViewProductForCategoryCommandAsync, allowsMultipleExecutions: false);
            ViewProductDetailsCommand = new AsyncCommand(ExecuteViewProductDetailsCommandAsync, allowsMultipleExecutions: false);
            ViewCartCommand = new AsyncCommand(ExecuteViewCartCommandAsync, allowsMultipleExecutions: false);
            ViewOrdersCommand = new AsyncCommand(ExecuteViewOrdersCommandAsync, allowsMultipleExecutions: false);
            ContactUsCommand = new AsyncCommand(ExecuteContactUsCommandAsync, allowsMultipleExecutions: false);
            LogoutCommand = new AsyncCommand(ExecuteLogoutCommandAsync, allowsMultipleExecutions: false);
        }

        public string Username { get; set; }

        public int CartItemsCount { get; set; } = 0;

        public CategoryInfo SelectedProductCategory { get; set; }

        public TrendingProduct SelectedProduct { get; set; }

        public ObservableRangeCollection<TrendingProduct> TrendingProducts { get; set; }

        public ObservableRangeCollection<CategoryInfo> Categories { get; set; }

        public IAsyncCommand ViewProductForCategoryCommand { get; }

        public IAsyncCommand ViewProductDetailsCommand { get; }

        public IAsyncCommand ViewCartCommand { get; }

        public IAsyncCommand ViewOrdersCommand { get; }

        public IAsyncCommand ContactUsCommand { get; }

        public IAsyncCommand LogoutCommand { get; set; }

        public Action CloseMenu { get; set; }

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            Username = _preferences.Get("userName", string.Empty);
            return Task.WhenAll(GetTrendingProducts(), GetCategories());
        }

        // FIXME - Doesn't get called when you navigate back from a modal page in iOS...
        // ... but does get called if it is a navigation page modal page
        public override Task OnAppearingAsync()
        {
            return GetCartItemsCount();
        }

        private async Task GetTrendingProducts()
        {
            var products = await _productService.GetTrendingProductsAsync();
            foreach (var product in products)
            {
                TrendingProducts.Add(product);
            }
        }

        private async Task GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }

        private async Task GetCartItemsCount()
        {
            var userId = _preferences.Get("userId", -1);
            var totalCartItem = await _shoppingCartService.GetTotalCartItemsAsync(userId);
            CartItemsCount = totalCartItem.TotalItems;
        }

        private Task ExecuteViewProductForCategoryCommandAsync()
        {
            if (SelectedProductCategory == null)
            {
                return Task.CompletedTask;
            }

            var parameters = new NavigationParameters
            {
                { nameof(CategoryInfo), SelectedProductCategory },
            };

            // Clear selection before navigating
            SelectedProductCategory = null;

            return NavigationService.NavigateAsync("NavigationPage/ProductListPage", parameters, useModalNavigation: true);
        }

        private Task ExecuteViewProductDetailsCommandAsync()
        {
            if (SelectedProduct == null)
            {
                return Task.CompletedTask;
            }

            var parameters = new NavigationParameters
            {
                { "ProductId", SelectedProduct.Id },
                { "ProductName", SelectedProduct.Name },
            };

            SelectedProduct = null;
            return NavigationService.NavigateAsync("NavigationPage/ProductDetailsPage", parameters, useModalNavigation: true);
        }

        private async Task ExecuteViewCartCommandAsync()
        {
            await NavigationService.NavigateAsync("NavigationPage/ShoppingCartPage", useModalNavigation: true);
            CloseMenu();
        }

        private async Task ExecuteContactUsCommandAsync()
        {
            await NavigationService.NavigateAsync("NavigationPage/ContactUsFormPage", useModalNavigation: true);
            CloseMenu();
        }

        private async Task ExecuteLogoutCommandAsync()
        {

        }

        private async Task ExecuteViewOrdersCommandAsync()
        {
            await NavigationService.NavigateAsync("NavigationPage/OrdersPage", useModalNavigation: true);
            CloseMenu();
        }
    }
}
