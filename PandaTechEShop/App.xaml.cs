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

            // NavigationService.NavigateAsync(PageConstants.MY_PAGE);
            NavigationService.NavigateAsync("NavigationPage/TestMainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<TestMainPage, TestMainPageViewModel>();

            containerRegistry.Register<IAccountService, AccountService>();
            containerRegistry.Register<ICategoryService, CategoryService>();
            containerRegistry.Register<IComplaintService, ComplaintService>();
            containerRegistry.Register<IPreferences, Preferences>();
            containerRegistry.Register<IProductService, ProductService>();
            containerRegistry.Register<IOrderService, OrderService>();
            containerRegistry.Register<IShoppingCartService, ShoppingCartService>();
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
