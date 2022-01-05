using System;
using System.Threading.Tasks;
using PandaTechEShop.Constants;
using PandaTechEShop.Models.Category;
using PandaTechEShop.Models.Product;
using PandaTechEShop.Services;
using PandaTechEShop.Services.Category;
using PandaTechEShop.Services.Product;
using PandaTechEShop.Services.ShoppingCart;
using PandaTechEShop.Services.Token;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PandaTechEShop.ViewModels.Home
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly ITokenService _tokenService;
        private readonly ITokenStorageService _tokenStorageService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IShoppingCartService _shoppingCartService;

        public HomePageViewModel(
            IBaseService baseService,
            ITokenService tokenService,
            ITokenStorageService tokenStorageService,
            IProductService productService,
            ICategoryService categoryService,
            IShoppingCartService shoppingCartService)
            : base(baseService)
        {
            Title = "Panda eShop";

            _tokenService = tokenService;
            _tokenStorageService = tokenStorageService;
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

        //public override void Initialize(INavigationParameters parameters)
        //{
        //    Username = _preferences.Get("userName", string.Empty);
        //    base.Initialize(parameters);
        //}

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            Username = _tokenService.GetUsername();
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
            var productsCollection = new ObservableRangeCollection<TrendingProduct>();
            foreach (var product in products)
            {
                productsCollection.Add(product);
            }

            TrendingProducts.ReplaceRange(productsCollection);
        }

        private async Task GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var catergoriesCollection = new ObservableRangeCollection<CategoryInfo>();
            foreach (var category in categories)
            {
                catergoriesCollection.Add(category);
            }

            Categories.ReplaceRange(catergoriesCollection);
        }

        private async Task GetCartItemsCount()
        {
            var userId = _tokenService.GetUserId();
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

            return NavigationService.NavigateAsync($"{NavigationConstants.NavigationPage}/{NavigationConstants.ProductListPage}", parameters, useModalNavigation: true);
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
            return NavigationService.NavigateAsync($"{NavigationConstants.NavigationPage}/{NavigationConstants.ProductDetailsPage}", parameters, useModalNavigation: true);
        }

        private async Task ExecuteViewCartCommandAsync()
        {
            await NavigationService.NavigateAsync($"{NavigationConstants.NavigationPage}/{NavigationConstants.ShoppingCartPage}", useModalNavigation: true);
            CloseMenu();
        }

        private async Task ExecuteContactUsCommandAsync()
        {
            await NavigationService.NavigateAsync($"{NavigationConstants.NavigationPage}/{NavigationConstants.ContactUsFormPage}", useModalNavigation: true);
            CloseMenu();
        }

        private Task ExecuteLogoutCommandAsync()
        {
            _tokenStorageService.DeleteToken();
            return NavigationService.NavigateAsync($"{NavigationConstants.RootNavigationPage}/{NavigationConstants.SignupPage}");
        }

        private async Task ExecuteViewOrdersCommandAsync()
        {
            await NavigationService.NavigateAsync($"{NavigationConstants.NavigationPage}/{NavigationConstants.OrdersPage}", useModalNavigation: true);
            CloseMenu();
        }
    }
}
