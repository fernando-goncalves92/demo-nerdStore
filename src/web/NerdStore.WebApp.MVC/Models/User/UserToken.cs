using System.Collections.Generic;

namespace NerdStore.WebApp.MVC.Models.User
{
    public class UserToken
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }
}
