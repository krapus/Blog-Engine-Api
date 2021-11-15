using BlogEngine.DataAccess.ExternalServices.Http.Interface;
using BlogEngine.DataAccess.ExternalServices.Users.Interface;
using BlogEngine.Model.Request;
using BlogEngine.Model.Response;
using BlogEngine.Model.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.ExternalServices.Users.Implementation
{
    public class AccountExternalService : IAccountExternalService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IHttpService _httpService;

        public AccountExternalService(IOptions<AppSettings> settings, IHttpService httpService)
        {
            _settings = settings;
            _httpService = httpService;
        }

        public async Task<UserSignUpExternalResponse> SignInUser(UserSignUpExternalRequest user)
        {
            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpRequest = this._httpService.CreateHttpRequestMessage(_settings.Value.ExternalService.FirebaseSignIn.Url, HttpMethod.Post, httpContent);
            var responseSignIn = await _httpService.ExecuteRequest(httpRequest);
            return JsonConvert.DeserializeObject<UserSignUpExternalResponse>(await responseSignIn.Content.ReadAsStringAsync());
        }

        public async Task<UserSignUpExternalResponse> SignUpUser(UserSignUpExternalRequest user)
        {
            StringContent httpContent = new StringContent(JsonConvert.SerializeObject(user), System.Text.Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpRequest = this._httpService.CreateHttpRequestMessage(_settings.Value.ExternalService.FirebaseSignUp.Url, HttpMethod.Post, httpContent);
            var responseSignUp = await this._httpService.ExecuteRequest(httpRequest);
            return JsonConvert.DeserializeObject<UserSignUpExternalResponse>(await responseSignUp.Content.ReadAsStringAsync());
        }
    }
}
