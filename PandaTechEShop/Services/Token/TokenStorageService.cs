using System;
using System.Threading.Tasks;
using PandaTechEShop.Constants;
using PandaTechEShop.Models.Account;
using PandaTechEShop.Services.MemoryCacheProvider;
using PandaTechEShop.Services.SecureStorage;

namespace PandaTechEShop.Services.Token
{
    public class TokenStorageService : ITokenStorageService
    {
        private readonly ISecureStorageService _secureStorage;
        private readonly IMemoryCacheProviderService _memoryCacheProviderService;

        public TokenStorageService(ISecureStorageService secureStorage, IMemoryCacheProviderService memoryCacheProviderService)
            : base()
        {
            _secureStorage = secureStorage;
            _memoryCacheProviderService = memoryCacheProviderService;
        }

        public async Task SaveToken(TokenInfo token)
        {
            await _secureStorage.SetAsync(TokenKeyConstants.AccessTokenKey, token.AccessToken);
            await _secureStorage.SetAsync(TokenKeyConstants.TokenExpirationTimeKey, token.ExpirationTime.ToString());
            await _secureStorage.SetAsync(TokenKeyConstants.UserIdKey, token.UserId.ToString());
            await _secureStorage.SetAsync(TokenKeyConstants.UserNameKey, token.UserName);

            LoadTokenIntoMemory(token);
        }

        public async Task DeleteToken()
        {
            await _secureStorage.SetAsync(TokenKeyConstants.AccessTokenKey, null);
            await _secureStorage.SetAsync(TokenKeyConstants.TokenExpirationTimeKey, null);
            await _secureStorage.SetAsync(TokenKeyConstants.UserIdKey, null);
            await _secureStorage.SetAsync(TokenKeyConstants.UserNameKey, null);

            _memoryCacheProviderService.Remove(TokenKeyConstants.AccessTokenKey);
            _memoryCacheProviderService.Remove(TokenKeyConstants.TokenExpirationTimeKey);
            _memoryCacheProviderService.Remove(TokenKeyConstants.UserIdKey);
            _memoryCacheProviderService.Remove(TokenKeyConstants.UserNameKey);
        }

        public async Task LoadTokenIntoMemory()
        {
            var token = new TokenInfo();
            token.AccessToken = await _secureStorage.GetAsync(TokenKeyConstants.AccessTokenKey);
            if (!string.IsNullOrEmpty(token.AccessToken))
            {
                token.ExpirationTime = Convert.ToInt32(await _secureStorage.GetAsync(TokenKeyConstants.TokenExpirationTimeKey));
                token.UserId = Convert.ToInt32(await _secureStorage.GetAsync(TokenKeyConstants.UserIdKey));
                token.UserName = await _secureStorage.GetAsync(TokenKeyConstants.UserNameKey);

                LoadTokenIntoMemory(token);
            }
        }

        public void RemoveTokenFromMemory()
        {
            _memoryCacheProviderService.Remove(TokenKeyConstants.AccessTokenKey);
            _memoryCacheProviderService.Remove(TokenKeyConstants.TokenExpirationTimeKey);
            _memoryCacheProviderService.Remove(TokenKeyConstants.UserIdKey);
            _memoryCacheProviderService.Remove(TokenKeyConstants.UserNameKey);
        }

        private void LoadTokenIntoMemory(TokenInfo token)
        {
            _memoryCacheProviderService.Set(TokenKeyConstants.AccessTokenKey, token.AccessToken);
            _memoryCacheProviderService.Set(TokenKeyConstants.TokenExpirationTimeKey, token.ExpirationTime);
            _memoryCacheProviderService.Set(TokenKeyConstants.UserIdKey, token.UserId);
            _memoryCacheProviderService.Set(TokenKeyConstants.UserNameKey, token.UserName);
        }
    }
}
