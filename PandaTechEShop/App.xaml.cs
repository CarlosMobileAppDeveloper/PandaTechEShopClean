using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism;
using Prism.Ioc;
using Prism.DryIoc;
using PandaTechEShop.Views;
using PandaTechEShop.ViewModels;

namespace PandaTechEShop
{
    public partial class App : PrismApplication 
    {
        //public App()
        //{
        //    InitializeComponent();

        //    MainPage = new MainPage();
        //}

        // Prism Setup Guid
        // https://www.c-sharpcorner.com/article/xamarin-forms-getting-starting-with-prism/

        public App(IPlatformInitializer platformInitializer = null) : base(platformInitializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            //NavigationService.NavigateAsync(PageConstants.MY_PAGE);
            NavigationService.NavigateAsync("NavigationPage/TestPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<TestPage, TestPageViewModel>();
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
