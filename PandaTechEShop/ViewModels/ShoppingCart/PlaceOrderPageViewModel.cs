using System.Threading.Tasks;
using PandaTechEShop.Controls.Popups;
using PandaTechEShop.Models.Order;
using PandaTechEShop.Services;
using PandaTechEShop.Services.Order;
using PandaTechEShop.Services.Token;
using PandaTechEShop.ViewModels.Base;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;

namespace PandaTechEShop.ViewModels.ShoppingCart
{
    public class PlaceOrderPageViewModel : BaseViewModel
    {
        private readonly ITokenService _tokenService;
        private readonly IOrderService _orderService;
        private double _orderTotal;

        public PlaceOrderPageViewModel(
            IBaseService baseService,
            ITokenService tokenService,
            IOrderService orderService)
            : base(baseService)
        {
            _tokenService = tokenService;
            _orderService = orderService;
            NavigateBackCommand = new AsyncCommand(ExecuteNavigateBackCommandAsync, allowsMultipleExecutions: false);
            PlaceOrderCommand = new AsyncCommand(ExecutePlaceOrderCommandAsync, allowsMultipleExecutions: false);
        }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public IAsyncCommand PlaceOrderCommand { get; set; }

        public IAsyncCommand NavigateBackCommand { get; }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (parameters.ContainsKey("OrderTotal"))
            {
                _orderTotal = parameters.GetValue<double>("OrderTotal");
            }
        }

        private async Task ExecutePlaceOrderCommandAsync()
        {
            FullName?.Trim();
            PhoneNumber?.Trim();
            Address?.Trim();

            if (!string.IsNullOrEmpty(FullName)
                && !string.IsNullOrEmpty(PhoneNumber)
                && !string.IsNullOrEmpty(Address))
            {
                var order = new OrderInfo
                {
                    FullName = FullName,
                    Phone = PhoneNumber,
                    Address = Address,
                    UserId = _tokenService.GetUserId(),
                    OrderTotal = (int)_orderTotal,
                };

                var response = await _orderService.PlaceOrderAsync(order);

                if (response != null)
                {
                    await PopupNavigation.PushAsync(new ToastPopup("Order Placed! Order Id: " + response.OrderId));
                    await Task.Delay(500);
                    await NavigationService.NavigateAsync("/NavigationPage/HomePage");
                }
                else
                {
                    await PopupNavigation.PushAsync(new ToastPopup("Something went wrong. Failed to place your order"));
                }
            }
        }

        private Task ExecuteNavigateBackCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }
    }
}
