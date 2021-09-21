using System;
using System.Threading.Tasks;

namespace PandaTechEShop.Services.Token
{
    public interface ITokenValidatorService
    {
        Task<bool> HasTokenExpired();
    }
}
