using NerdStore.WebApp.MVC.Models.User;
using System.Net.Http;
using System.Threading.Tasks;
using NerdStore.WebApp.MVC.Models;

namespace NerdStore.WebApp.MVC.Services.Authentication
{
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserLoginResponse> CreateAccount(UserRegister user)
        {   
            var response = await _httpClient.PostAsync("https://localhost:44301/api/v1/authentication/create-account", ConvertToStringContent(user));
            
            if (!VerifyResponseErrors(response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = await GetResponse<ResponseResult>(response)
                };
            }

            return await GetResponse<UserLoginResponse>(response);
        }

        public async Task<UserLoginResponse> Login(UserLogin user)
        {
            var response = await _httpClient.PostAsync("https://localhost:44301/api/v1/authentication/login", ConvertToStringContent(user));

            if (!VerifyResponseErrors(response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = await GetResponse<ResponseResult>(response)
                };
            }

            return await GetResponse<UserLoginResponse>(response);
        }
    }
}
