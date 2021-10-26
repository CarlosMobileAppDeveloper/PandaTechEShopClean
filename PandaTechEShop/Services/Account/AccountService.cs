using System.Threading.Tasks;
using PandaTechEShop.Exceptions;
using PandaTechEShop.Models.Account;
using PandaTechEShop.Services.RequestProvider;
using PandaTechEShop.Services.Token;

namespace PandaTechEShop.Services.Account
{
    public class AccountService : IAccountService
    {
        private const string _apiUrlBase = AppSettings.ApiUrl + "/api/accounts";

        private readonly IRequestProvider _requestProvider;
        private readonly ITokenStorageService _tokenStorageService;

        public AccountService(IRequestProvider requestProvider, ITokenStorageService tokenStorageService)
            : base()
        {
            _requestProvider = requestProvider;
            _tokenStorageService = tokenStorageService;
        }

        public async Task<bool> RegisterUserAsync(string name, string email, string password)
        {
            var newUser = new Register()
            {
                Name = name,
                Email = email,
                Password = password,
            };

            try
            {
                await _requestProvider.PostAsync<object>(uri: _apiUrlBase + "/register", data: newUser);
                return true;
            }
            catch (HttpRequestExceptionEx)
            {
                return false;
            }
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var credentials = new Login()
            {
                Email = email,
                Password = password,
            };

            try
            {
                var response = await _requestProvider.PostAsync<TokenInfo>(_apiUrlBase + "/login", data: credentials);
                await _tokenStorageService.SaveToken(response);
                return true;
            }
            catch (HttpRequestExceptionEx)
            {
                return false;
            }
        }
    }
}
