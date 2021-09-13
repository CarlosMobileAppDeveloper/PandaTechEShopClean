using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PandaTechEShop.Models.Complaint;
using PandaTechEShop.Services.Preferences;

namespace PandaTechEShop.Services.Complaint
{
    public class ComplaintService : IComplaintService
    {
        private const string _apiUrlBase = AppSettings.ApiUrl + "/api/Complaints";

        private readonly IPreferences _preferences;

        public ComplaintService(IPreferences preferences)
            : base()
        {
            _preferences = preferences;
        }

        public async Task<bool> RegisterComplaintAsync(ComplaintInfo complaint)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _preferences.Get("accessToken", string.Empty));
            var json = JsonConvert.SerializeObject(complaint);
            var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(_apiUrlBase, content: content);
            return response.IsSuccessStatusCode;
        }
    }
}
