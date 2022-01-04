using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using PandaTechEShop.Exceptions;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PandaTechEShop.Services.RequestProvider
{
    public class RequestProvider : IRequestProvider
    {
        public Task<T> GetAsync<T>(string uri, string token = "") where T : class
        {
            CheckNetworkConnectivity();
            HttpClient httpClient = CreateHttpClient(token);
            return ProcessGetRequest<T>(httpClient, uri);
        }

        public Task<T> GetAndRetryAsync<T>(string uri, int retryCount, string token = "",
            Func<Exception, int, Task> onRetry = null) where T : class
        {
            CheckNetworkConnectivity();
            HttpClient httpClient = CreateHttpClient(token);
            var func = new Func<Task<T>>(() => ProcessGetRequest<T>(httpClient, uri));
            return Retry(func, retryCount, onRetry);
        }

        public Task<T> GetWaitAndTryAsync<T>(string uri, Func<int, TimeSpan> sleepDurationProvider, int retryCount,
            string token = "", Func<Exception, TimeSpan, Task> onWaitAndRetry = null) where T : class
        {
            CheckNetworkConnectivity();
            HttpClient httpClient = CreateHttpClient(token);
            var func = new Func<Task<T>>(() => ProcessGetRequest<T>(httpClient, uri));
            return WaitAndRetry(func, sleepDurationProvider, retryCount, onWaitAndRetry);
        }

        public Task<T> PostAsync<T>(string uri, string token = "", object data = null) where T : class
        {
            CheckNetworkConnectivity();
            HttpClient httpClient = CreateHttpClient(token);
            return ProcessPostRequest<T>(httpClient, uri, data);
        }

        //public Task<T> PostAndRetryAsync<T>(this HttpClient httpClient, string path, int retryCount, object data = null, Func<Exception, int, Task> onRetry = null) where T : class
        //{
        //    CheckNetworkConnectivity();
        //    var func = new Func<Task<T>>(() => ProcessPostRequest<T>(httpClient, path, data));
        //    return Retry(func, retryCount, onRetry);
        //}

        //public Task<T> PostWaitAndTryAsync<T>(this HttpClient httpClient, string path, Func<int, TimeSpan> sleepDurationProvider, int retryCount, object data = null, Func<Exception, TimeSpan, Task> onWaitAndRetry = null) where T : class
        //{
        //    CheckNetworkConnectivity();
        //    var func = new Func<Task<T>>(() => ProcessPostRequest<T>(httpClient, path, data));
        //    return WaitAndRetry(func, sleepDurationProvider, retryCount, onWaitAndRetry);
        //}

        public Task<T> PutAsync<T>(string uri, string token = "", object data = null) where T : class
        {
            CheckNetworkConnectivity();
            HttpClient httpClient = CreateHttpClient(token);
            return ProcessPutRequest<T>(httpClient, uri, data);
        }

        //public Task<T> PutAndRetryAsync<T>(this HttpClient httpClient, string path, int retryCount, object data = null, Func<Exception, int, Task> onRetry = null) where T : class
        //{
        //    CheckNetworkConnectivity();
        //    var func = new Func<Task<T>>(() => ProcessPutRequest<T>(httpClient, path, data));
        //    return Retry(func, retryCount, onRetry);
        //}

        //public Task<T> PutWaitAndTryAsync<T>(this HttpClient httpClient, string path, Func<int, TimeSpan> sleepDurationProvider, int retryCount, object data = null, Func<Exception, TimeSpan, Task> onWaitAndRetry = null) where T : class
        //{
        //    CheckNetworkConnectivity();
        //    var func = new Func<Task<T>>(() => ProcessPutRequest<T>(httpClient, path, data));
        //    return WaitAndRetry(func, sleepDurationProvider, retryCount, onWaitAndRetry);
        //}

        public Task<T> DeleteAsync<T>(string uri, string token = "", object data = null) where T : class
        {
            CheckNetworkConnectivity();
            HttpClient httpClient = CreateHttpClient(token);
            return ProcessDeleteRequest<T>(httpClient, uri, data);
        }

        //public Task<T> DeleteAndRetryAsync<T>(this HttpClient httpClient, string path, int retryCount, object data = null, Func<Exception, int, Task> onRetry = null) where T : class
        //{
        //    CheckNetworkConnectivity();
        //    var func = new Func<Task<T>>(() => ProcessDeleteRequest<T>(httpClient, path, data));
        //    return Retry(func, retryCount, onRetry);
        //}

        //public Task<T> DeleteWaitAndTryAsync<T>(this HttpClient httpClient, string path, Func<int, TimeSpan> sleepDurationProvider, int retryCount, object data = null, Func<Exception, TimeSpan, Task> onWaitAndRetry = null) where T : class
        //{
        //    CheckNetworkConnectivity();
        //    var func = new Func<Task<T>>(() => ProcessDeleteRequest<T>(httpClient, path, data));
        //    return WaitAndRetry(func, sleepDurationProvider, retryCount, onWaitAndRetry);
        //}

        private HttpClient CreateHttpClient(string token = "")
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return httpClient;
        }

        private async Task<T> ProcessGetRequest<T>(HttpClient httpClient, string path)
        {
            var response = await httpClient.GetAsync(path);

            var responseContent = await response.Content.ReadAsStringAsync();

            CheckStatusCode(response, responseContent);

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        private async Task<T> ProcessPostRequest<T>(HttpClient httpClient, string path, object data = null)
        {
            var content = data is HttpContent httpContent
                ? httpContent
                : new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(path, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            CheckStatusCode(response, responseContent);

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        private async Task<T> ProcessPutRequest<T>(HttpClient httpClient, string path, object data = null)
            where T : class
        {
            var content = data is HttpContent httpContent
                ? httpContent
                : new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(path, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            CheckStatusCode(response, responseContent);

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        private async Task<T> ProcessDeleteRequest<T>(HttpClient httpClient, string path, object data = null)
            where T : class
        {
            var req = new HttpRequestMessage(HttpMethod.Delete, path)
            {
                Content = data is HttpContent content
                    ? content
                    : new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"),
            };

            var response = await httpClient.SendAsync(req);

            var responseContent = await response.Content.ReadAsStringAsync();

            CheckStatusCode(response, responseContent);

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        private Task<T> Retry<T>(Func<Task<T>> func, int retryCount, Func<Exception, int, Task> onRetry)
        {
            onRetry = onRetry ?? OnRetry;
            return Policy
                .Handle<Exception>()
                .RetryAsync(retryCount, onRetry)
                .ExecuteAsync(func);
        }

        private Task<T> WaitAndRetry<T>(Func<Task<T>> func, Func<int, TimeSpan> sleepDurationProvider, int retryCount,
            Func<Exception, TimeSpan, Task> onRetryAsync)
        {
            return Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(retryCount, sleepDurationProvider, onRetryAsync)
                .ExecuteAsync(func);
        }

        private Task OnRetry(Exception exception, int count)
        {
            return Task.CompletedTask;
        }

        private void CheckNetworkConnectivity()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None || Connectivity.NetworkAccess == NetworkAccess.Unknown)
                throw new ConnectivityException("No Network Connection.");
        }

        private void CheckStatusCode(HttpResponseMessage response, string content)
        {
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(content, response.RequestMessage.RequestUri);
                }

                throw new HttpRequestExceptionEx(response.StatusCode, response.RequestMessage.RequestUri, content);
            }
        }
    }
}