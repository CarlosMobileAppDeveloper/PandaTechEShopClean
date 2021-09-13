using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PandaTechEShop.Models.ShoppingCart;
using PandaTechEShop.Services.Preferences;

namespace PandaTechEShop.Services.ShoppingCart
{
    public class ShoppingCartService : IShoppingCartService
    {
        private const string _apiUrlBase = AppSettings.ApiUrl + "/api/ShoppingCartItems";

        private readonly IPreferences _preferences;

        public ShoppingCartService(IPreferences preferences)
            : base()
        {
            _preferences = preferences;
        }

        public async Task<bool> AddItemsInCartAsync(AddToCart addToCart)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var json = JsonConvert.SerializeObject(addToCart);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(_apiUrlBase, content: content);
            return response.IsSuccessStatusCode;
        }

        public async Task<CartSubTotal> GetShoppingCartSubTotalAsync(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(_apiUrlBase + "/SubTotal/" + userId);
            return JsonConvert.DeserializeObject<CartSubTotal>(response);
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(_apiUrlBase + "/" + userId);
            return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(response);
        }

        public async Task<TotalCartItem> GetTotalCartItemsAsync(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(_apiUrlBase + "/TotalItems/" + userId);
            return JsonConvert.DeserializeObject<TotalCartItem>(response);
        }

        public async Task<bool> ClearShoppingCartAsync(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.DeleteAsync(_apiUrlBase + "/" + userId);
            return response.IsSuccessStatusCode;
        }
    }
}
