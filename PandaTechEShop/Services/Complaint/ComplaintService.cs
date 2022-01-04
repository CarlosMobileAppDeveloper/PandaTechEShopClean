using System.Threading.Tasks;
using PandaTechEShop.Exceptions;
using PandaTechEShop.Models.Complaint;
using PandaTechEShop.Services.RequestProvider;
using PandaTechEShop.Services.Token;

namespace PandaTechEShop.Services.Complaint
{
    public class ComplaintService : IComplaintService
    {
        private const string _apiUrlBase = AppSettings.ApiUrl + "/api/Complaints";

        private readonly IRequestProvider _requestProvider;
        private readonly ITokenService _tokenService;

        public ComplaintService(IRequestProvider requestProvider, ITokenService tokenService)
            : base()
        {
            _requestProvider = requestProvider;
            _tokenService = tokenService;
        }

        public async Task<bool> RegisterComplaintAsync(ComplaintInfo complaint)
        {
            try
            {
                var accessToken = _tokenService.GetAccessToken();
                await _requestProvider.PostAsync<object>(uri: _apiUrlBase + "/register", token: accessToken,
                    data: complaint);
                return true;
            }
            catch (HttpRequestExceptionEx)
            {
                return false;
            }

            //var httpClient = new HttpClient();
            //var accessToken = _tokenService.GetAccessToken();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            //var json = JsonConvert.SerializeObject(complaint);
            //var content = new StringContent(json, encoding: System.Text.Encoding.UTF8, "application/json");
            //var response = await httpClient.PostAsync(_apiUrlBase, content: content);
            //return response.IsSuccessStatusCode;
        }
    }
}