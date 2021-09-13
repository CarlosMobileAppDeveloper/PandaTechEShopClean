using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PandaTechEShop.Models.Account;
using PandaTechEShop.Services.Preferences;
using Refit;

namespace PandaTechEShop.Services.Account
{
    public class AccountService : IAccountService
    {
        private const string _apiUrlBase = AppSettings.ApiUrl + "/api/accounts";

        private readonly IPreferences _preferences;

        public AccountService(IPreferences preferences)
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

            // TODO - How to do Refit way?
            // var service = RestService.For<IAccountApi>(AppSettings.ApiUrl);
            // var response = await service.RegisterUser(newRegistration);
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(newRegistration);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(_apiUrlBase + "/register", content: content);
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
            var response = await httpClient.PostAsync(_apiUrlBase + "/login", content: content);
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
    }
}
