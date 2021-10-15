namespace NerdStore.Identity.API.ViewModels
{
    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel User { get; set; }
    }
}
