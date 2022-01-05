using System.Threading.Tasks;

namespace PandaTechEShop.Services.Token
{
    public interface ITokenService
    {
        string GetAccessToken();
        int GetTokenExpirationTime();
        int GetUserId();
        string GetUsername();
        Task<bool> UpdateAccessToken();
    }
}
