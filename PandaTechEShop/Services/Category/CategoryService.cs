using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PandaTechEShop.Models.Category;
using PandaTechEShop.Services.Preferences;

namespace PandaTechEShop.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private const string _apiUrlBase = AppSettings.ApiUrl + "/api/categories";

        private readonly IPreferences _preferences;

        public CategoryService(IPreferences preferences)
            : base()
        {
            _preferences = preferences;
        }

        public async Task<List<CategoryInfo>> GetCategoriesAsync()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(_apiUrlBase);
            return JsonConvert.DeserializeObject<List<CategoryInfo>>(response);
        }
    }
}
