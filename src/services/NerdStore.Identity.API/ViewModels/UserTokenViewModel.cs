using System.Collections.Generic;

namespace NerdStore.Identity.API.ViewModels
{
    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }
}
