using System;
using System.Threading.Tasks;

namespace PandaTechEShop.Services.Account
{
    public interface IAccountService
    {
        Task<bool> RegisterUserAsync(string name, string email, string password);
        Task<bool> LoginAsync(string email, string password);
    }
}
