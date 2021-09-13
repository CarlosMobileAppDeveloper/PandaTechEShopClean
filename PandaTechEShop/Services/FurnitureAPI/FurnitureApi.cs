using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PandaTechEShop.Models.Account;
using PandaTechEShop.Services.Preferences;
using System.Net.Http.Headers;
using PandaTechEShop.Models.Category;
using System.Collections.Generic;
using PandaTechEShop.Models.Product;
using PandaTechEShop.Models.ShoppingCart;
using PandaTechEShop.Models.Order;
using PandaTechEShop.Models.Complaint;

namespace PandaTechEShop.Services.FurnitureAPI
{
    public class FurnitureApi : IFurnitureApi
    {
        private readonly IPreferences _preferences;

        public FurnitureApi(IPreferences preferences)
            : base()
        {
            _preferences = preferences;
        }

        public async Task<bool> RegisterUserAsync(string name, string email, string password)
        {
            var newRegistration = new Register()
            {
                Name = name,
                Email = email,
                Password = password,
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(newRegistration);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "/api/accounts/register", content: content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var login = new Login()
            {
                Email = email,
                Password = password,
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "/api/accounts/login", content: content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);
            _preferences.Set("accessToken", result.AccessToken);
            _preferences.Set("userId", result.UserId);
            _preferences.Set("userName", result.UserName);
            return true;
        }

        public async Task<List<CategoryInfo>> GetCategoriesAsync()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/categories");
            return JsonConvert.DeserializeObject<List<CategoryInfo>>(response);
        }

        public async Task<ProductInfo> GetProductByIdAsync(int productId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/products/" + productId);
            return JsonConvert.DeserializeObject<ProductInfo>(response);
        }

        public async Task<List<ProductInfo>> GetProductsByCategoryAsync(int categoryId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/products/productsbycategory/" + categoryId);
            return JsonConvert.DeserializeObject<List<ProductInfo>>(response);
        }

        public async Task<List<TrendingProduct>> GetTrendingProductsAsync()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/products/trendingproducts");
            return JsonConvert.DeserializeObject<List<TrendingProduct>>(response);
        }

        public async Task<bool> AddItemsInCartAsync(AddToCart addToCart)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var json = JsonConvert.SerializeObject(addToCart);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "/api/ShoppingCartItems", content: content);
            return response.IsSuccessStatusCode;
        }

        public async Task<CartSubTotal> GetShoppingCartSubTotalAsync(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/ShoppingCartItems/SubTotal/" + userId);
            return JsonConvert.DeserializeObject<CartSubTotal>(response);
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/ShoppingCartItems/" + userId);
            return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(response);
        }

        public async Task<TotalCartItem> GetTotalCartItemsAsync(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/ShoppingCartItems/TotalItems/" + userId);
            return JsonConvert.DeserializeObject<TotalCartItem>(response);
        }

        public async Task<bool> ClearShoppingCartAsync(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + "/api/ShoppingCartItems/" + userId);
            return response.IsSuccessStatusCode;
        }

        public async Task<OrderResponse> PlaceOrderAsync(OrderInfo order)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "/api/Orders", content: content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrderResponse>(jsonResult);
        }

        public async Task<List<OrderByUser>> GetOrdersByUserAsync(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/Orders/OrdersByUser" + userId);
            return JsonConvert.DeserializeObject<List<OrderByUser>>(response);
        }

        public async Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/Orders/OrderDetails/" + orderId);
            return JsonConvert.DeserializeObject<List<OrderDetail>>(response);
        }

        public async Task<bool> RegisterComplaintAsync(ComplaintInfo complaint)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var json = JsonConvert.SerializeObject(complaint);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "/api/Complaints", content: content);
            return response.IsSuccessStatusCode;
        }
    }
}
