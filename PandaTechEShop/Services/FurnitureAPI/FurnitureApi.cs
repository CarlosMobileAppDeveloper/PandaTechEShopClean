using PandaTechEShop.Services.Token;
using PandaTechEShop.Services.RequestProvider;

namespace PandaTechEShop.Services.FurnitureAPI
{
    public class FurnitureApi : IFurnitureApi
    {
        private readonly IRequestProvider _requestProvider;
        private readonly ITokenService _tokenService;

        public FurnitureApi(IRequestProvider requestProvider, ITokenService tokenService)
            : base()
        {
            _requestProvider = requestProvider;
            _tokenService = tokenService;
        }

        //public async Task<List<CategoryInfo>> GetCategoriesAsync()
        //{
        //    var httpClient = new HttpClient();
        //    var accessToken = _tokenService.GetAccessToken();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/categories");
        //    return JsonConvert.DeserializeObject<List<CategoryInfo>>(response);
        //}

        //public async Task<ProductInfo> GetProductByIdAsync(int productId)
        //{
        //    var httpClient = new HttpClient();
        //    var accessToken = _tokenService.GetAccessToken();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/products/" + productId);
        //    return JsonConvert.DeserializeObject<ProductInfo>(response);
        //}

        //public async Task<List<ProductInfo>> GetProductsByCategoryAsync(int categoryId)
        //{
        //    var httpClient = new HttpClient();
        //    var accessToken = _tokenService.GetAccessToken();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/products/productsbycategory/" + categoryId);
        //    return JsonConvert.DeserializeObject<List<ProductInfo>>(response);
        //}

        //public async Task<List<TrendingProduct>> GetTrendingProductsAsync()
        //{
        //    var httpClient = new HttpClient();
        //    var accessToken = _tokenService.GetAccessToken();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/products/trendingproducts");
        //    return JsonConvert.DeserializeObject<List<TrendingProduct>>(response);
        //}

        //public async Task<bool> AddItemsInCartAsync(AddToCart addToCart)
        //{
        //    var httpClient = new HttpClient();
        //    var accessToken = _tokenService.GetAccessToken();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var json = JsonConvert.SerializeObject(addToCart);
        //    var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
        //    var response = await httpClient.PostAsync(AppSettings.ApiUrl + "/api/ShoppingCartItems", content: content);
        //    return response.IsSuccessStatusCode;
        //}

        //public async Task<CartSubTotal> GetShoppingCartSubTotalAsync(int userId)
        //{
        //    var httpClient = new HttpClient();
        //    var accessToken = _tokenService.GetAccessToken();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/ShoppingCartItems/SubTotal/" + userId);
        //    return JsonConvert.DeserializeObject<CartSubTotal>(response);
        //}

        //public async Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync(int userId)
        //{
        //    var httpClient = new HttpClient();
        //    var accessToken = _tokenService.GetAccessToken();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/ShoppingCartItems/" + userId);
        //    return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(response);
        //}

        //public async Task<TotalCartItem> GetTotalCartItemsAsync(int userId)
        //{
        //    var httpClient = new HttpClient();
        //    var accessToken = _tokenService.GetAccessToken();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/ShoppingCartItems/TotalItems/" + userId);
        //    return JsonConvert.DeserializeObject<TotalCartItem>(response);
        //}

        //public async Task<bool> ClearShoppingCartAsync(int userId)
        //{
        //    var httpClient = new HttpClient();
        //    var accessToken = _tokenService.GetAccessToken();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + "/api/ShoppingCartItems/" + userId);
        //    return response.IsSuccessStatusCode;
        //}

        //public async Task<OrderResponse> PlaceOrderAsync(OrderInfo order)
        //{
        //    var httpClient = new HttpClient();
        //    var accessToken = _tokenService.GetAccessToken();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var json = JsonConvert.SerializeObject(order);
        //    var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
        //    var response = await httpClient.PostAsync(AppSettings.ApiUrl + "/api/Orders", content: content);
        //    var jsonResult = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<OrderResponse>(jsonResult);
        //}

        //public async Task<List<OrderByUser>> GetOrdersByUserAsync(int userId)
        //{
        //    var httpClient = new HttpClient();
        //    var accessToken = _tokenService.GetAccessToken();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/Orders/OrdersByUser" + userId);
        //    return JsonConvert.DeserializeObject<List<OrderByUser>>(response);
        //}

        //public async Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId)
        //{
        //    var httpClient = new HttpClient();
        //    var accessToken = _tokenService.GetAccessToken();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "/api/Orders/OrderDetails/" + orderId);
        //    return JsonConvert.DeserializeObject<List<OrderDetail>>(response);
        //}

        //public async Task<bool> RegisterComplaintAsync(ComplaintInfo complaint)
        //{
        //    var httpClient = new HttpClient();
        //    var accessToken = _tokenService.GetAccessToken();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
        //    var json = JsonConvert.SerializeObject(complaint);
        //    var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
        //    var response = await httpClient.PostAsync(AppSettings.ApiUrl + "/api/Complaints", content: content);
        //    return response.IsSuccessStatusCode;
        //}
    }
}
