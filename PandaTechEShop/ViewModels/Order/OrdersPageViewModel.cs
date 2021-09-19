using System;
using System.Threading.Tasks;
using PandaTechEShop.Models.Order;
using PandaTechEShop.Services.Order;
using PandaTechEShop.Services.Preferences;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Rg.Plugins.Popup.Contracts;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PandaTechEShop.ViewModels.Order
{
    public class OrdersPageViewModel : BaseViewModel
    {
        private readonly IPreferences _preferences;
        private readonly IOrderService _orderService;

        public OrdersPageViewModel(
            INavigationService navigationService,
            IPopupNavigation popupNavigation,
            IPreferences preferences,
            IOrderService orderService)
            : base(navigationService, popupNavigation)
        {
            _preferences = preferences;
            _orderService = orderService;
            NavigateBackCommand = new AsyncCommand(ExecuteNavigateBackCommandAsync, allowsMultipleExecutions: false);
            ViewOrderDetailsCommand = new AsyncCommand<OrderByUser>(order => ExecuteViewOrderDetailsCommandAsync(order), allowsMultipleExecutions: false);
            Orders = new ObservableRangeCollection<OrderByUser>();
        }

        public IAsyncCommand NavigateBackCommand { get; }

        public IAsyncCommand<OrderByUser> ViewOrderDetailsCommand { get; }

        public OrderByUser SelectedOrder { get; set; }

        public ObservableRangeCollection<OrderByUser> Orders { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                GetOrdersAsync().ConfigureAwait(false);
            }
        }

        private async Task GetOrdersAsync()
        {
            var ordersList = await _orderService.GetOrdersByUserAsync(_preferences.Get("userId", -1));
            var orders = new ObservableRangeCollection<OrderByUser>();
            foreach (var order in ordersList)
            {
                orders.Add(order);
            }

            Orders = orders;
        }

        private async Task ExecuteViewOrderDetailsCommandAsync(OrderByUser order)
        {
            if (order == null)
            {
                return;
            }

            var parameters = new NavigationParameters
            {
                { "OrderId", order.Id },
                { "OrderTotal", order.OrderTotal },
            };

            SelectedOrder = null;
            await NavigationService.NavigateAsync("OrderDetailsPage", parameters);
        }

        private Task ExecuteNavigateBackCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }
    }
}
