using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Mediator;
using NerdStore.Order.API.Application.Commands;
using NerdStore.Order.API.Application.Queries.Order;
using NerdStore.Order.API.Application.Queries.Voucher;
using NerdStore.WebAPI.Core.Controllers;
using NerdStore.WebAPI.Core.Facilities;
using System.Threading.Tasks;

namespace NerdStore.Order.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrderController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;
        private readonly IOrderQuery _orderQuery;

        public OrderController(IMediatorHandler mediator, IAspNetUser user, IOrderQuery orderQuery)
        {
            _mediator = mediator;
            _user = user;
            _orderQuery = orderQuery;
        }

        [HttpPost()]
        public async Task<IActionResult> AddOrder(AddOrderCommand order)
        {
            order.CustomerId = _user.GetUserId();

            return CustomResponse(await _mediator.SendCommand(order));
        }

        [HttpGet("customer/last")]
        public async Task<IActionResult> LastOrder()
        {
            var order = await _orderQuery.GetLastOrderByCustomerIdAsync(_user.GetUserId());

            return order == null ? NotFound() : CustomResponse(order);
        }

        [HttpGet("customer/all")]
        public async Task<IActionResult> CustomerOrders()
        {
            var orders = await _orderQuery.GetOrdersByCustomerIdAsync(_user.GetUserId());

            return orders == null ? NotFound() : CustomResponse(orders);
        }
    }
}
