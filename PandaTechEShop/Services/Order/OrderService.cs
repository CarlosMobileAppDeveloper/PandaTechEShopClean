using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.Exceptions;
using PandaTechEShop.Models.Order;
using PandaTechEShop.Services.RequestProvider;
using PandaTechEShop.Services.Token;

namespace PandaTechEShop.Services.Order
{
    public class OrderService : IOrderService
    {
        private const string _apiUrlBase = AppSettings.ApiUrl + "/api/Orders";

        private readonly IRequestProvider _requestProvider;
        private readonly ITokenService _tokenService;

        public OrderService(IRequestProvider requestProvider, ITokenService tokenService)
            : base()
        {
            _requestProvider = requestProvider;
            _tokenService = tokenService;
        }

        public Task<OrderResponse> PlaceOrderAsync(OrderInfo order)
        {
            try
            {
                var accessToken = _tokenService.GetAccessToken();
                return _requestProvider.PostAsync<OrderResponse>(uri: _apiUrlBase, token: accessToken, data: order);
            }
            catch (HttpRequestExceptionEx)
            {
                return Task.FromResult<OrderResponse>(null);
            }

            //var httpClient = new HttpClient();
            //var accessToken = _tokenService.GetAccessToken();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //var json = JsonConvert.SerializeObject(order);
            //var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            //var response = await httpClient.PostAsync(_apiUrlBase, content: content);
            //var jsonResult = await response.Content.ReadAsStringAsync();
            //return JsonConvert.DeserializeObject<OrderResponse>(jsonResult);
        }

        public Task<List<OrderByUser>> GetOrdersByUserAsync(int userId)
        {
            try
            {
                var accessToken = _tokenService.GetAccessToken();
                return _requestProvider.GetAsync<List<OrderByUser>>(uri: _apiUrlBase + "/OrdersByUser/" + userId,
                    token: accessToken);
            }
            catch (HttpRequestExceptionEx)
            {
                return Task.FromResult<List<OrderByUser>>(null);
            }

            //var httpClient = new HttpClient();
            //var accessToken = _tokenService.GetAccessToken();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //var response = await httpClient.GetStringAsync(_apiUrlBase + "/OrdersByUser/" + userId);
            //return JsonConvert.DeserializeObject<List<OrderByUser>>(response);
        }

        public Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId)
        {
            try
            {
                var accessToken = _tokenService.GetAccessToken();
                return _requestProvider.GetAsync<List<OrderDetail>>(uri: _apiUrlBase + "/OrderDetails/" + orderId,
                    token: accessToken);
            }
            catch (HttpRequestExceptionEx)
            {
                return Task.FromResult<List<OrderDetail>>(null);
            }

            //var httpClient = new HttpClient();
            //var accessToken = _tokenService.GetAccessToken();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //var response = await httpClient.GetStringAsync(_apiUrlBase + "/OrderDetails/" + orderId);
            //return JsonConvert.DeserializeObject<List<OrderDetail>>(response);
        }
    }
}