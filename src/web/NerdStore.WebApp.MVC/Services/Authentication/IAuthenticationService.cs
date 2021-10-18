using NerdStore.WebApp.MVC.Models.User;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<UserLoginResponse> CreateAccount(UserRegister user);
        Task<UserLoginResponse> Login(UserLogin user);
    }
}
