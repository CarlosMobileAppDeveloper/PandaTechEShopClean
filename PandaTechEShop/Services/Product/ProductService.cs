using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.Exceptions;
using PandaTechEShop.Models.Product;
using PandaTechEShop.Services.RequestProvider;
using PandaTechEShop.Services.Token;

namespace PandaTechEShop.Services.Product
{
    public class ProductService : IProductService
    {
        private const string _apiUrlBase = AppSettings.ApiUrl + "/api/products";

        private readonly IRequestProvider _requestProvider;
        private readonly ITokenService _tokenService;

        public ProductService(IRequestProvider requestProvider, ITokenService tokenService)
            : base()
        {
            _requestProvider = requestProvider;
            _tokenService = tokenService;
        }

        public Task<ProductInfo> GetProductByIdAsync(int productId)
        {
            try
            {
                var accessToken = _tokenService.GetAccessToken();
                return _requestProvider.GetAsync<ProductInfo>(uri: _apiUrlBase + "/" + productId, token: accessToken);
            }
            catch (HttpRequestExceptionEx)
            {
                return Task.FromResult<ProductInfo>(null);
            }

            //var httpClient = new HttpClient();
            //var accessToken = _tokenService.GetAccessToken();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //var response = await httpClient.GetStringAsync(_apiUrlBase + "/" + productId);
            //return JsonConvert.DeserializeObject<ProductInfo>(response);
        }

        public Task<List<ProductByCategory>> GetProductsByCategoryAsync(int categoryId)
        {
            try
            {
                var accessToken = _tokenService.GetAccessToken();
                return _requestProvider.GetAsync<List<ProductByCategory>>(uri: _apiUrlBase + "/productsbycategory/" + categoryId, token: accessToken);
            }
            catch (HttpRequestExceptionEx)
            {
                return Task.FromResult<List<ProductByCategory>>(null);
            }

            //var httpClient = new HttpClient();
            //var accessToken = _tokenService.GetAccessToken();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //var response = await httpClient.GetStringAsync(_apiUrlBase + "/productsbycategory/" + categoryId);
            //return JsonConvert.DeserializeObject<List<ProductByCategory>>(response);
        }

        public Task<List<TrendingProduct>> GetTrendingProductsAsync()
        {
            try
            {
                var accessToken = _tokenService.GetAccessToken();
                return _requestProvider.GetAsync<List<TrendingProduct>>(uri: _apiUrlBase + "/trendingproducts", token: accessToken);
            }
            catch (HttpRequestExceptionEx)
            {
                return Task.FromResult<List<TrendingProduct>>(null);
            }

            //var httpClient = new HttpClient();
            //var accessToken = _tokenService.GetAccessToken();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //var response = await httpClient.GetStringAsync(_apiUrlBase + "/trendingproducts");
            //return JsonConvert.DeserializeObject<List<TrendingProduct>>(response);
        }
    }
}
