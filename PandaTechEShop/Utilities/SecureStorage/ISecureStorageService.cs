using System.Threading.Tasks;

namespace PandaTechEShop.Utilities.SecureStorage
{
    public interface ISecureStorageService
    {
        Task<bool> SetAsync(string key, string value);
        Task<string> GetAsync(string key);
        Task<bool> Remove(string key);
    }
}
