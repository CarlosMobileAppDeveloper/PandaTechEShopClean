using System;
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

            // NavigationService.NavigateAsync(PageConstants.MY_PAGE);
            //NavigationService.NavigateAsync("NavigationPage/TestMainPage");

            NavigationService.NavigateAsync("InitPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<InitPage, InitPageViewModel>();
            containerRegistry.RegisterForNavigation<TestMainPage, TestMainPageViewModel>();
            containerRegistry.RegisterForNavigation<CreateAccountPage, CreateAccountPageViewModel>();
            containerRegistry.RegisterForNavigation<SignupPage, SignupPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<ProductListPage, ProductListPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductDetailsPage, ProductDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<ShoppingCartPage, ShoppingCartPageViewModel>();
            containerRegistry.RegisterForNavigation<PlaceOrderPage, PlaceOrderPageViewModel>();
            containerRegistry.RegisterForNavigation<OrdersPage, OrdersPageViewModel>();
            containerRegistry.RegisterForNavigation<OrderDetailsPage, OrderDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<ContactUsFormPage, ContactUsFormPageViewModel>();

            containerRegistry.Register<IAccountService, AccountService>();
            containerRegistry.Register<ICategoryService, CategoryService>();
            containerRegistry.Register<IComplaintService, ComplaintService>();
            containerRegistry.Register<IPreferences, Preferences>();
            containerRegistry.Register<IProductService, ProductService>();
            containerRegistry.Register<IOrderService, OrderService>();
            containerRegistry.Register<IShoppingCartService, ShoppingCartService>();
            containerRegistry.Register<ITokenValidatorService, TokenValidatorService>();

            containerRegistry.RegisterInstance(PopupNavigation.Instance);
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
