using System;
using System.Threading.Tasks;

namespace PandaTechEShop.Services.FurnitureAPI
{
    public interface IFurnitureApi
    {
        Task<bool> RegisterUserAsync(string name, string email, string password);
    }
}
