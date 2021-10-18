using System.Text.Json.Serialization;

namespace NerdStore.WebApp.MVC.Models.User
{
    public class UserLoginResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken User { get; set; }
        
        [JsonIgnore]
        public ResponseResult ResponseResult { get; set; }
    }
}
