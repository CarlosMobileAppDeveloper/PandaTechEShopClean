using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PandaTechEShop.Models.Product;
using PandaTechEShop.Services.Preferences;

namespace PandaTechEShop.Services.Product
{
    public class ProductService : IProductService
    {
        private const string _apiUrlBase = AppSettings.ApiUrl + "/api/products";

        private readonly IPreferences _preferences;

        public ProductService(IPreferences preferences)
            : base()
        {
            _preferences = preferences;
        }

        public async Task<ProductInfo> GetProductByIdAsync(int productId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(_apiUrlBase + "/" + productId);
            return JsonConvert.DeserializeObject<ProductInfo>(response);
        }

        public async Task<List<ProductInfo>> GetProductsByCategoryAsync(int categoryId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(_apiUrlBase + "/productsbycategory/" + categoryId);
            return JsonConvert.DeserializeObject<List<ProductInfo>>(response);
        }

        public async Task<List<TrendingProduct>> GetTrendingProductsAsync()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(_apiUrlBase + "/trendingproducts");
            return JsonConvert.DeserializeObject<List<TrendingProduct>>(response);
        }
    }
}
