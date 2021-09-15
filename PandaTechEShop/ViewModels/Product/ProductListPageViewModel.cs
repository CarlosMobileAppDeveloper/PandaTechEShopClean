using System;
using System.Threading.Tasks;
using PandaTechEShop.Models.Category;
using PandaTechEShop.Models.Product;
using PandaTechEShop.Services.Preferences;
using PandaTechEShop.Services.Product;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PandaTechEShop.ViewModels.Product
{
    public class ProductListPageViewModel : BaseViewModel
    {
        private readonly IPreferences _preferences;
        private readonly IProductService _productService;
        private CategoryInfo _productCategory;

        public ProductListPageViewModel(
            INavigationService navigationService,
            IPopupNavigation popupNavigation,
            IPreferences preferences,
            IProductService productService)
            : base(navigationService, popupNavigation)
        {
            _preferences = preferences;
            _productService = productService;
            Products = new ObservableRangeCollection<ProductInfo>();
            NavigateBackCommand = new AsyncCommand(ExecuteNavigateBackCommandAsync, allowsMultipleExecutions: false);
        }

        public ObservableRangeCollection<ProductInfo> Products { get; set; }

        public IAsyncCommand NavigateBackCommand { get; }

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(nameof(CategoryInfo)))
            {
                _productCategory = parameters.GetValue<CategoryInfo>(nameof(CategoryInfo));
                Title = _productCategory.Name;
            }

            return LoadProducts();
        }

        private async Task LoadProducts()
        {
            var products = await _productService.GetProductsByCategoryAsync(_productCategory.Id);
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        private Task ExecuteNavigateBackCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }
    }
}
