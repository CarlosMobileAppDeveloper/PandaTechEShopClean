using System.Threading.Tasks;
using PandaTechEShop.Models.Order;
using PandaTechEShop.Services;
using PandaTechEShop.Services.Order;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PandaTechEShop.ViewModels.Order
{
    public class OrderDetailsPageViewModel : BaseViewModel
    {
        private readonly IOrderService _orderService;
        private int _orderId;

        public OrderDetailsPageViewModel(
            IBaseService baseService,
            IOrderService orderService)
            : base(baseService)
        {
            _orderService = orderService;
            NavigateBackCommand = new AsyncCommand(ExecuteNavigateBackCommandAsync, allowsMultipleExecutions: false);
            OrderDetails = new ObservableRangeCollection<OrderDetail>();
        }

        public double OrderTotal { get; set; }

        public ObservableRangeCollection<OrderDetail> OrderDetails { get; set; }

        public IAsyncCommand NavigateBackCommand { get; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                if (parameters.ContainsKey("OrderId") && parameters.ContainsKey("OrderTotal"))
                {
                    _orderId = parameters.GetValue<int>("OrderId");
                    OrderTotal = parameters.GetValue<double>("OrderTotal");
                    GetOrderDetails().ConfigureAwait(false);
                }
            }
        }

        private async Task GetOrderDetails()
        {
            var orderDetails = await _orderService.GetOrderDetailsAsync(_orderId);
            var tempOrderDetailsCollection = new ObservableRangeCollection<OrderDetail>();
            foreach (var orderItem in orderDetails)
            {
                tempOrderDetailsCollection.Add(orderItem);
            }

            OrderDetails = tempOrderDetailsCollection;
        }

        private Task ExecuteNavigateBackCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }
    }
}
