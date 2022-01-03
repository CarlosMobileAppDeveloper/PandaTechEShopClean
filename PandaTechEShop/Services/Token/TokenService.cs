using System;
using System.Threading.Tasks;
using PandaTechEShop.Constants;
using PandaTechEShop.Services.Account;
using PandaTechEShop.Utilities.MemoryCacheProvider;
using PandaTechEShop.Utilities.SecureStorage;

namespace PandaTechEShop.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IMemoryCacheProviderService _memoryCacheProviderService;
        private readonly IAccountService _accountService;
        private readonly ISecureStorageService _secureStorageService;

        public TokenService(
            IMemoryCacheProviderService memoryCacheProviderService,
            IAccountService accountService,
            ISecureStorageService secureStorageService)
        {
            _memoryCacheProviderService = memoryCacheProviderService;
            _accountService = accountService;
            _secureStorageService = secureStorageService;
        }

        public string GetAccessToken()
        {
            return _memoryCacheProviderService.Get<string>(TokenKeyConstants.AccessTokenKey);
        }

        public int GetUserId()
        {
            return _memoryCacheProviderService.Get<int>(TokenKeyConstants.UserIdKey);
        }

        public int GetTokenExpirationTime()
        {
            return _memoryCacheProviderService.Get<int>(TokenKeyConstants.TokenExpirationTimeKey);
        }

        public string GetUsername()
        {
            return _memoryCacheProviderService.Get<string>(TokenKeyConstants.UserNameKey);
        }

        public async Task<bool> UpdateAccessToken()
        {
            // HACK - For testing purposes only. Used for refreshing token but this isn't implemented properly yet
            var email = await _secureStorageService.GetAsync(SecureStorageConstants.EmailKey);
            var password = await _secureStorageService.GetAsync(SecureStorageConstants.PasswordKey);

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                return await _accountService.LoginAsync(email, password);
            }

            return false;
        }
    }
}
