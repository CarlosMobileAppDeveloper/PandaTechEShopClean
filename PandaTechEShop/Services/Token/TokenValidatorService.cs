using System;
using System.Threading.Tasks;
using PandaTechEShop.Services.Preferences;
using UnixTimeStamp;

namespace PandaTechEShop.Services.Token
{
    public class TokenValidatorService : ITokenValidatorService
    {
        private readonly IPreferences _preferencesService;

        public TokenValidatorService(IPreferences preferences)
        {
            _preferencesService = preferences;
        }

        public Task<bool> HasTokenExpired()
        {
            var expirationTime = _preferencesService.Get("tokenExpirationTime", 0);
            var currentTime = UnixTime.GetCurrentTime();

            if (expirationTime < currentTime)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
