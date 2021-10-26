using System.Threading.Tasks;

namespace PandaTechEShop.Services.SecureStorage
{
    public interface ISecureStorageService
    {
        Task<bool> SetAsync(string key, string value);
        Task<string> GetAsync(string key);
    }
}
