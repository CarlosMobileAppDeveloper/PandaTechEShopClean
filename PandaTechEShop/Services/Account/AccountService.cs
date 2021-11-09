using System.Threading.Tasks;
using PandaTechEShop.Constants;
using PandaTechEShop.Exceptions;
using PandaTechEShop.Models.Account;
using PandaTechEShop.Services.RequestProvider;
using PandaTechEShop.Services.SecureStorage;
using PandaTechEShop.Services.Token;

namespace PandaTechEShop.Services.Account
{
    public class AccountService : IAccountService
    {
        private const string _apiUrlBase = AppSettings.ApiUrl + "/api/accounts";

        private readonly IRequestProvider _requestProvider;
        private readonly ITokenStorageService _tokenStorageService;
        private readonly ISecureStorageService _secureStorageService;

        public AccountService(IRequestProvider requestProvider, ITokenStorageService tokenStorageService, ISecureStorageService secureStorageService)
            : base()
        {
            _requestProvider = requestProvider;
            _tokenStorageService = tokenStorageService;
            _secureStorageService = secureStorageService;
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
            catch (HttpRequestExceptionEx exception)
            {
                var badexception = exception;
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

                // HACK - For testing purposes only. Used for refreshing token but this isn't implemented properly yet
                await _secureStorageService.SetAsync(SecureStorageConstants.EmailKey, email);
                await _secureStorageService.SetAsync(SecureStorageConstants.PasswordKey, password);

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
