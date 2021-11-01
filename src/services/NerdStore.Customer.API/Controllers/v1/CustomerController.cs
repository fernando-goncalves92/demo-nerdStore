using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Mediator;
using NerdStore.Customer.API.Commands;
using NerdStore.Customer.API.Data;
using NerdStore.WebAPI.Core.Controllers;
using NerdStore.WebAPI.Core.Facilities;
using System.Threading.Tasks;

namespace NerdStore.Customer.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CustomerController : MainController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;

        public CustomerController(ICustomerRepository customerRepository, IMediatorHandler mediator, IAspNetUser user)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
            _user = user;
        }

        [HttpGet("address")]
        public async Task<IActionResult> GetAddress()
        {
            var address = await _customerRepository.GetAddressById(_user.GetUserId());

            return address == null ? NotFound() : CustomResponse(address);
        }

        [HttpPost("address")]
        public async Task<IActionResult> AddAddress(AddCustomerAddressCommand address)
        {
            address.CustomerId = _user.GetUserId();

            return CustomResponse(await _mediator.SendCommand(address));
        }
    }
}
