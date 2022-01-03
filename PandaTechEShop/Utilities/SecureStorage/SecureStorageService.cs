using System;
using System.Threading.Tasks;

namespace PandaTechEShop.Services.SecureStorage
{
    // FIXME - How to handle failures better?
    public class SecureStorageService : ISecureStorageService
    {
        public async Task<bool> SetAsync(string key, string value)
        {
            try
            {
                await Xamarin.Essentials.SecureStorage.SetAsync(key, value);
                return true;
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
                return false;
            }
        }
        
        public Task<bool> Remove(string key)
        {
            try
            {
                Xamarin.Essentials.SecureStorage.Remove(key);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
                return Task.FromResult(true);
            }
        }

        public Task<string> GetAsync(string key)
        {
            try
            {
                return Xamarin.Essentials.SecureStorage.GetAsync(key);
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
                return Task.FromResult<string>(null);
            }
        }
    }
}
