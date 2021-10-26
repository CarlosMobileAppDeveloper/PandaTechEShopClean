using System;
using PandaTechEShop.Constants;
using PandaTechEShop.Services.MemoryCacheProvider;

namespace PandaTechEShop.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IMemoryCacheProviderService _memoryCacheProviderService;

        public TokenService(IMemoryCacheProviderService memoryCacheProviderService)
        {
            _memoryCacheProviderService = memoryCacheProviderService;
        }

        public string GetAccessToken()
        {
            return _memoryCacheProviderService.Get<string>(TokenKeyConstants.AccessTokenKey);
        }

        public int GetUserId()
        {
            return _memoryCacheProviderService.Get<int>(TokenKeyConstants.TokenExpirationTimeKey);
        }

        public int GetTokenExpirationTime()
        {
            return _memoryCacheProviderService.Get<int>(TokenKeyConstants.UserIdKey);
        }

        public string GetUsername()
        {
            return _memoryCacheProviderService.Get<string>(TokenKeyConstants.UserNameKey);
        }
    }
}
