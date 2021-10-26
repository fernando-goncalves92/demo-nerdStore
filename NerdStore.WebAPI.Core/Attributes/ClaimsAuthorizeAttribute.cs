using Microsoft.AspNetCore.Mvc;
using NerdStore.WebAPI.Core.Filters;
using System.Security.Claims;

namespace NerdStore.WebAPI.Core.Attributes
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequestClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
