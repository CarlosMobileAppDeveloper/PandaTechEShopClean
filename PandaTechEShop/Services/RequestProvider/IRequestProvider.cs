using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PandaTechEShop.Services.RequestProvider
{
    public interface IRequestProvider
    {
        //Task<T> GetAsync<T>(string uri, string token = "");

        //Task<T> PostAsync<T>(string uri, object data, string token = "", string header = "");

        ////Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret);

        //Task<T> PutAsync<T>(string uri, object data, string token = "", string header = "");

        //Task DeleteAsync(string uri, string token = "");

        Task<T> GetAsync<T>(string uri, string token = "") where T : class;

        Task<T> GetAndRetryAsync<T>(string uri, int retryCount, string token = "", Func<Exception, int, Task> onRetry = null) where T : class;

        Task<T> GetWaitAndTryAsync<T>(string uri, Func<int, TimeSpan> sleepDurationProvider, int retryCount, string token = "", Func<Exception, TimeSpan, Task> onWaitAndRetry = null) where T : class;

        Task<T> PostAsync<T>(string uri, string token = "", object data = null) where T : class;

            //Task<T> PostAndRetryAsync<T>(this HttpClient httpClient, string path, int retryCount, object data = null, Func<Exception, int, Task> onRetry = null) where T : class;

            //Task<T> PostWaitAndTryAsync<T>(this HttpClient httpClient, string path, Func<int, TimeSpan> sleepDurationProvider, int retryCount, object data = null, Func<Exception, TimeSpan, Task> onWaitAndRetry = null) where T : class;

        Task<T> PutAsync<T>(string uri, string token = "", object data = null) where T : class;

            //Task<T> PutAndRetryAsync<T>(this HttpClient httpClient, string path, int retryCount, object data = null, Func<Exception, int, Task> onRetry = null) where T : class;

            //Task<T> PutWaitAndTryAsync<T>(this HttpClient httpClient, string path, Func<int, TimeSpan> sleepDurationProvider, int retryCount, object data = null, Func<Exception, TimeSpan, Task> onWaitAndRetry = null) where T : class;

        Task<T> DeleteAsync<T>(string uri, string token = "", object data = null) where T : class;

            //Task<T> DeleteAndRetryAsync<T>(this HttpClient httpClient, string path, int retryCount, object data = null, Func<Exception, int, Task> onRetry = null) where T : class;

            //Task<T> DeleteWaitAndTryAsync<T>(this HttpClient httpClient, string path, Func<int, TimeSpan> sleepDurationProvider, int retryCount, object data = null, Func<Exception, TimeSpan, Task> onWaitAndRetry = null) where T : class;

    }
}
