using System.Threading.Tasks;
using UnixTimeStamp;

namespace PandaTechEShop.Services.Token
{
    public class TokenValidatorService : ITokenValidatorService
    {
        private readonly ITokenService _tokenService;

        public TokenValidatorService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public Task<bool> HasTokenExpired()
        {
            var expirationTime = _tokenService.GetTokenExpirationTime();
            var currentTime = UnixTime.GetCurrentTime();

            if (expirationTime < currentTime)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
