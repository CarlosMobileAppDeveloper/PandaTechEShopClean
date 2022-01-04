using System.Collections.Generic;
using System.Threading.Tasks;
using PandaTechEShop.Exceptions;
using PandaTechEShop.Models.Category;
using PandaTechEShop.Services.RequestProvider;
using PandaTechEShop.Services.Token;

namespace PandaTechEShop.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private const string _apiUrlBase = AppSettings.ApiUrl + "/api/categories";

        private readonly IRequestProvider _requestProvider;
        private readonly ITokenService _tokenService;

        public CategoryService(IRequestProvider requestProvider, ITokenService tokenService)
            : base()
        {
            _requestProvider = requestProvider;
            _tokenService = tokenService;
        }

        public Task<List<CategoryInfo>> GetCategoriesAsync()
        {
            try
            {
                var accessToken = _tokenService.GetAccessToken();
                return _requestProvider.GetAsync<List<CategoryInfo>>(uri: _apiUrlBase, token: accessToken);
            }
            catch (HttpRequestExceptionEx)
            {
                return Task.FromResult<List<CategoryInfo>>(null);
            }

            //var httpClient = new HttpClient();
            //var accessToken = _tokenService.GetAccessToken();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //var response = await httpClient.GetStringAsync(_apiUrlBase);
            //return JsonConvert.DeserializeObject<List<CategoryInfo>>(response);
        }
    }
}