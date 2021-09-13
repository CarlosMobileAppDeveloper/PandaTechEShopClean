
using System.Threading.Tasks;
using PandaTechEShop.Models.Account;
using Refit;

namespace PandaTechEShop.Services.Account
{
    public interface IAccountApi
    {
        [Post("/api/accounts/register")]
        Task RegisterUser([Body] Register newReistration);

        [Post("/api/accounts/login")]
        Task<Token> Login([Body] Login login);
    }
}
