using Microsoft.AspNetCore.Mvc;

namespace NerdStore.Customer.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello");
        }
    }
}
