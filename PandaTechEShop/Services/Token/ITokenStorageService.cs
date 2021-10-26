using System;
using System.Threading.Tasks;
using PandaTechEShop.Models.Account;

namespace PandaTechEShop.Services.Token
{
    public interface ITokenStorageService
    {
        Task SaveToken(TokenInfo token);
        Task DeleteToken();
        Task LoadTokenIntoMemory();
        void RemoveTokenFromMemory();
    }
}
