using System;
using PandaTechEShop.Constants;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism;
using Prism.Ioc;
using Prism.DryIoc;
using PandaTechEShop.Views;
using PandaTechEShop.ViewModels;
using PandaTechEShop.Services.Preferences;
using PandaTechEShop.Services.Account;
using PandaTechEShop.Services.Category;
using PandaTechEShop.Services.Product;
using PandaTechEShop.Services.ShoppingCart;
using PandaTechEShop.Services.Order;
using PandaTechEShop.Services.Complaint;
using PandaTechEShop.ViewModels.Account;
using PandaTechEShop.Views.Account;
using Rg.Plugins.Popup.Services;
using PandaTechEShop.ViewModels.Home;
using PandaTechEShop.Views.Home;
using PandaTechEShop.ViewModels.Init;
using PandaTechEShop.Views.Init;
using PandaTechEShop.Views.Product;
using PandaTechEShop.ViewModels.Product;
using PandaTechEShop.ViewModels.ShoppingCart;
using PandaTechEShop.Views.ShoppingCart;
using PandaTechEShop.Views.Order;
using PandaTechEShop.ViewModels.Order;
using PandaTechEShop.Views.ContactUs;
using PandaTechEShop.ViewModels.ContactUs;
using PandaTechEShop.Services.Token;
using PandaTechEShop.Services;
using PandaTechEShop.Services.RequestProvider;
using PandaTechEShop.Utilities.Dialog;
using PandaTechEShop.Utilities.Logger;
using PandaTechEShop.Utilities.SecureStorage;
using PandaTechEShop.Utilities.MemoryCacheProvider;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;

namespace PandaTechEShop
{
    public partial class App : PrismApplication
    {
        // Prism Setup Guid
        // https://www.c-sharpcorner.com/article/xamarin-forms-getting-starting-with-prism/
        public App(IPlatformInitializer platformInitializer = null)
            : base(platformInitializer)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            XF.Material.Forms.Material.Use("Material.Configuration");
            NavigationService.NavigateAsync(NavigationConstants.InitPage);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>(NavigationConstants.NavigationPage);
            containerRegistry.RegisterForNavigation<InitPage, InitPageViewModel>(NavigationConstants.InitPage);
            //containerRegistry.RegisterForNavigation<TestMainPage, TestMainPageViewModel>();
            containerRegistry.RegisterForNavigation<SignupPage, SignupPageViewModel>(NavigationConstants.SignupPage);
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>(NavigationConstants.LoginPage);
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>(NavigationConstants.HomePage);
            containerRegistry.RegisterForNavigation<ProductListPage, ProductListPageViewModel>(NavigationConstants.ProductListPage);
            containerRegistry.RegisterForNavigation<ProductDetailsPage, ProductDetailsPageViewModel>(NavigationConstants.ProductDetailsPage);
            containerRegistry.RegisterForNavigation<ShoppingCartPage, ShoppingCartPageViewModel>(NavigationConstants.ShoppingCartPage);
            containerRegistry.RegisterForNavigation<PlaceOrderPage, PlaceOrderPageViewModel>(NavigationConstants.PlaceOrderPage);
            containerRegistry.RegisterForNavigation<OrdersPage, OrdersPageViewModel>(NavigationConstants.OrdersPage);
            containerRegistry.RegisterForNavigation<OrderDetailsPage, OrderDetailsPageViewModel>(NavigationConstants.OrderDetailsPage);
            containerRegistry.RegisterForNavigation<ContactUsFormPage, ContactUsFormPageViewModel>(NavigationConstants.ContactUsFormPage);

            containerRegistry.Register<IBaseService, BaseService>();
            containerRegistry.Register<ILogger, Logger>();
            containerRegistry.Register<IAccountService, AccountService>();
            containerRegistry.Register<ICategoryService, CategoryService>();
            containerRegistry.Register<IComplaintService, ComplaintService>();
            containerRegistry.Register<IProductService, ProductService>();
            containerRegistry.Register<IOrderService, OrderService>();
            containerRegistry.Register<IShoppingCartService, ShoppingCartService>();
            containerRegistry.Register<ITokenValidatorService, TokenValidatorService>();
            containerRegistry.Register<ITokenService, TokenService>();
            containerRegistry.Register<IDialogService, DialogService>();

            containerRegistry.RegisterInstance(PopupNavigation.Instance);
            containerRegistry.RegisterInstance(MaterialDialog.Instance);

            containerRegistry.RegisterSingleton<IRequestProvider, RequestProvider>();
            containerRegistry.RegisterSingleton<IPreferences, Preferences>(); // TODO - Should this be a singleton?
            containerRegistry.RegisterSingleton<ISecureStorageService, SecureStorageService>(); // TODO - Should this be a singleton?
            containerRegistry.RegisterSingleton<IMemoryCacheProviderService, MemoryCacheProviderService>();
            containerRegistry.RegisterSingleton<ITokenStorageService, TokenStorageService>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
