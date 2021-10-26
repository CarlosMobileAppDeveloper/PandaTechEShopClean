using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.Exceptions;
using PandaTechEShop.Models.ShoppingCart;
using PandaTechEShop.Services.RequestProvider;
using PandaTechEShop.Services.Token;

namespace PandaTechEShop.Services.ShoppingCart
{
    public class ShoppingCartService : IShoppingCartService
    {
        private const string _apiUrlBase = AppSettings.ApiUrl + "/api/ShoppingCartItems";

        private readonly IRequestProvider _requestProvider;
        private readonly ITokenService _tokenService;

        public ShoppingCartService(IRequestProvider requestProvider, ITokenService tokenService)
            : base()
        {
            _requestProvider = requestProvider;
            _tokenService = tokenService;
        }

        public async Task<bool> AddItemsInCartAsync(AddToCart addToCart)
        {
            try
            {
                var accessToken = _tokenService.GetAccessToken();
                await _requestProvider.PostAsync<object>(uri: _apiUrlBase, token: accessToken);
                return true;
            }
            catch (HttpRequestExceptionEx)
            {
                return false;
            }

            //var httpClient = new HttpClient();
            //var accessToken = _tokenService.GetAccessToken();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //var json = JsonConvert.SerializeObject(addToCart);
            //var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            //var response = await httpClient.PostAsync(_apiUrlBase, content: content);
            //return response.IsSuccessStatusCode;
        }

        public Task<CartSubTotal> GetShoppingCartSubTotalAsync(int userId)
        {
            try
            {
                var accessToken = _tokenService.GetAccessToken();
                return _requestProvider.GetAsync<CartSubTotal>(uri: _apiUrlBase + "/SubTotal/" + userId, token: accessToken);
            }
            catch (HttpRequestExceptionEx)
            {
                return Task.FromResult<CartSubTotal>(null);
            }

            //var httpClient = new HttpClient();
            //var accessToken = _tokenService.GetAccessToken();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //var response = await httpClient.GetStringAsync(_apiUrlBase + "/SubTotal/" + userId);
            //return JsonConvert.DeserializeObject<CartSubTotal>(response);
        }

        public Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync(int userId)
        {
            try
            {
                var accessToken = _tokenService.GetAccessToken();
                return _requestProvider.GetAsync<List<ShoppingCartItem>>(uri: _apiUrlBase + "/" + userId, token: accessToken);
            }
            catch (HttpRequestExceptionEx)
            {
                return Task.FromResult<List<ShoppingCartItem>>(null);
            }

            //var httpClient = new HttpClient();
            //var accessToken = _tokenService.GetAccessToken();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //var response = await httpClient.GetStringAsync(_apiUrlBase + "/" + userId);
            //return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(response);
        }

        public Task<TotalCartItem> GetTotalCartItemsAsync(int userId)
        {
            try
            {
                var accessToken = _tokenService.GetAccessToken();
                return _requestProvider.GetAsync<TotalCartItem>(uri: _apiUrlBase + "/TotalItems/" + userId, token: accessToken);
            }
            catch (HttpRequestExceptionEx)
            {
                return Task.FromResult<TotalCartItem>(null);
            }

            //var httpClient = new HttpClient();
            //var accessToken = _tokenService.GetAccessToken();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //var response = await httpClient.GetStringAsync(_apiUrlBase + "/TotalItems/" + userId);
            //return JsonConvert.DeserializeObject<TotalCartItem>(response);
        }

        public async Task<bool> ClearShoppingCartAsync(int userId)
        {
            try
            {
                var accessToken = _tokenService.GetAccessToken();
                await _requestProvider.DeleteAsync<object>(uri: _apiUrlBase + "/" + userId, token: accessToken);
                return true;
            }
            catch (HttpRequestExceptionEx)
            {
                return false;
            }

            //var httpClient = new HttpClient();
            //var accessToken = _tokenService.GetAccessToken();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //var response = await httpClient.DeleteAsync(_apiUrlBase + "/" + userId);
            //return response.IsSuccessStatusCode;
        }
    }
}
