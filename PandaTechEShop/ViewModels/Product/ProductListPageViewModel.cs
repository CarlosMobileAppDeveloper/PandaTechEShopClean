﻿using System;
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
        private CategoryInfo _category;

        public ProductListPageViewModel(
            INavigationService navigationService,
            IPopupNavigation popupNavigation,
            IPreferences preferences,
            IProductService productService)
            : base(navigationService, popupNavigation)
        {
            _preferences = preferences;
            _productService = productService;
            ProductsByCategory = new ObservableRangeCollection<ProductByCategory>();
            NavigateBackCommand = new AsyncCommand(ExecuteNavigateBackCommandAsync, allowsMultipleExecutions: false);
            ViewProductDetailsCommand = new AsyncCommand(ExecuteViewProductDetailsCommandAsync, allowsMultipleExecutions: false);
        }

        public ProductByCategory SelectedProduct { get; set; }

        public ObservableRangeCollection<ProductByCategory> ProductsByCategory { get; set; }

        public IAsyncCommand NavigateBackCommand { get; }

        public IAsyncCommand ViewProductDetailsCommand { get; }

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(nameof(CategoryInfo)))
            {
                _category = parameters.GetValue<CategoryInfo>(nameof(CategoryInfo));
                Title = _category.Name;
            }

            return LoadProducts();
        }

        private async Task LoadProducts()
        {
            var products = await _productService.GetProductsByCategoryAsync(_category.Id);
            foreach (var product in products)
            {
                ProductsByCategory.Add(product);
            }
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

        private Task ExecuteNavigateBackCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }
    }
}
