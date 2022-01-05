using System.Threading.Tasks;
using PandaTechEShop.Constants;
using PandaTechEShop.Models.Order;
using PandaTechEShop.Services;
using PandaTechEShop.Services.Order;
using PandaTechEShop.Services.Token;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PandaTechEShop.ViewModels.Order
{
    public class OrdersPageViewModel : BaseViewModel
    {
        private readonly ITokenService _tokenService;
        private readonly IOrderService _orderService;

        public OrdersPageViewModel(
            IBaseService baseService,
            ITokenService tokenService,
            IOrderService orderService)
            : base(baseService)
        {
            _tokenService = tokenService;
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
            var ordersList = await _orderService.GetOrdersByUserAsync(_tokenService.GetUserId());
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
            await NavigationService.NavigateAsync($"{NavigationConstants.OrderDetailsPage}", parameters);
        }

        private Task ExecuteNavigateBackCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }
    }
}
