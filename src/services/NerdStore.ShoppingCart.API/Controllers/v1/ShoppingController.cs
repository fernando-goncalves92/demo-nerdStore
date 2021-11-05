using Microsoft.AspNetCore.Mvc;
using NerdStore.WebAPI.Core.Facilities;
using NerdStore.WebApp.MVC.Controllers;
using System;
using System.Threading.Tasks;

namespace NerdStore.ShoppingCart.API.Controllers.v1
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

        [HttpGet("cart")]
        public Task<IActionResult> GetShoppingCart()
        {
            return Task.FromResult<IActionResult>(Ok("GetShoppingCart"));
        }

        [HttpPost("cart")]
        public Task<IActionResult> AddShoppingCartItem()
        {
            return Task.FromResult<IActionResult>(Ok("AddShoppingCartItem"));
        }

        [HttpPut("cart/{productId:guid}")]
        public Task<IActionResult> UpdateShoppingCartItem(Guid productId)
        {
            return Task.FromResult<IActionResult>(Ok("UpdateShoppingCartItem"));
        }

        [HttpDelete("cart/{productId:guid}")]
        public Task<IActionResult> RemoveShoppingCartItem(Guid productId)
        {
            return Task.FromResult<IActionResult>(Ok("RemoveShoppingCartItem"));
        }
    }
}
