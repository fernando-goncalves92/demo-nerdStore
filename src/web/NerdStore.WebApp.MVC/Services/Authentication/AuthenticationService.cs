using NerdStore.WebApp.MVC.Models.User;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace NerdStore.WebApp.MVC.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserLoginResponse> CreateAccount(UserRegister user)
        {   
            var response = await _httpClient.PostAsync("https://localhost:44301/api/v1/authentication/create-account", CreateSringContent(user));

            return JsonSerializer.Deserialize<UserLoginResponse>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<UserLoginResponse> Login(UserLogin user)
        {
            var response = await _httpClient.PostAsync("https://localhost:44301/api/v1/authentication/login", CreateSringContent(user));

            return JsonSerializer.Deserialize<UserLoginResponse>(
                await response.Content.ReadAsStringAsync(), 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private StringContent CreateSringContent<T>(T obj)
        {
            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
        }
    }
}
