using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PandaTechEShop.Models.Order;
using PandaTechEShop.Services.Preferences;

namespace PandaTechEShop.Services.Order
{
    public class OrderService : IOrderService
    {
        private const string _apiUrlBase = AppSettings.ApiUrl + "/api/Orders";

        private readonly IPreferences _preferences;

        public OrderService(IPreferences preferences)
            : base()
        {
            _preferences = preferences;
        }

        public async Task<OrderResponse> PlaceOrderAsync(OrderInfo order)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(_apiUrlBase, content: content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrderResponse>(jsonResult);
        }

        public async Task<List<OrderByUser>> GetOrdersByUserAsync(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(_apiUrlBase + "/OrdersByUser" + userId);
            return JsonConvert.DeserializeObject<List<OrderByUser>>(response);
        }

        public async Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(_apiUrlBase + "/OrderDetails/" + orderId);
            return JsonConvert.DeserializeObject<List<OrderDetail>>(response);
        }
    }
}
