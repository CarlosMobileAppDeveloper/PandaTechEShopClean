using System;
using System.Collections.Generic;
using PandaTechEShop.ViewModels.Home;
using Xamarin.Forms;

namespace PandaTechEShop.Views.Home
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as HomePageViewModel;
            if (viewModel != null)
            {
                viewModel.CloseMenu = CloseMenu;
            }
        }

        private async void TapMenu_Tapped(object sender, EventArgs e)
        {
            GridOverlay.IsVisible = true;
            await SlMenu.TranslateTo(0, 0, 400, Easing.Linear);
        }

        private async void TapCloseMenu_Tapped(object sender, EventArgs e)
        {
            await SlMenu.TranslateTo(-250, 0, 400, Easing.Linear);
            GridOverlay.IsVisible = false;
        }

        private async void OpenMenu()
        {
            GridOverlay.IsVisible = true;
            await SlMenu.TranslateTo(0, 0, 400, Easing.Linear);
        }

        private async void CloseMenu()
        {
            if (GridOverlay.IsVisible)
            {
                await SlMenu.TranslateTo(-250, 0, 400, Easing.Linear);
                GridOverlay.IsVisible = false;
            }
        }
    }
}
