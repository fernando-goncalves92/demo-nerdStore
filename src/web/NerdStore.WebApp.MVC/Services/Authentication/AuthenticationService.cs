using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NerdStore.WebApp.MVC.Facilities;
using NerdStore.WebApp.MVC.Models.User;
using NerdStore.Core.Communication;

namespace NerdStore.WebApp.MVC.Services.Authentication
{
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient, IOptions<UrlAccess> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.AuthenticationUrl);
        }

        public async Task<UserLoginResponse> CreateAccount(UserRegister user)
        {   
            var response = await _httpClient.PostAsync("/api/v1/authentication/create-account", ConvertToStringContent(user));
            
            if (!IsSuccessResponseStatusCode(response))
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
            var response = await _httpClient.PostAsync("/api/v1/authentication/login", ConvertToStringContent(user));

            if (!IsSuccessResponseStatusCode(response))
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
