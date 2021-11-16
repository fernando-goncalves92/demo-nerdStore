using Microsoft.AspNetCore.Mvc;
using NerdStore.WebAPI.Core.Controllers;
using NerdStore.WebAPI.Core.Facilities;

namespace NerdStore.BFF.Shopping.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ShoppingController : MainController
    {
        private readonly IAspNetUser _aspNetUser;

        public ShoppingController(IAspNetUser aspNetUser)
        {
            _aspNetUser = aspNetUser;
        }
    }
}
