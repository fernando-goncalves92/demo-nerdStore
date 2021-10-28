using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace NerdStore.WebAPI.Core.Filters
{
    public class RequestClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequestClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);

                return;
            }

            if (!context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value.Contains(_claim.Value)))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
