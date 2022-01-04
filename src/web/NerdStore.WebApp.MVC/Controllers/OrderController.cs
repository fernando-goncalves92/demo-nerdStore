using Microsoft.AspNetCore.Mvc;
using NerdStore.WebApp.MVC.Models.Order;
using NerdStore.WebApp.MVC.Services.Customer;
using NerdStore.WebApp.MVC.Services.ShoppingBff;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class OrderController : MainController
    {
        private readonly ICustomerService _customerService;
        private readonly IShoppingBffService _shoppingBffService;

        public OrderController(ICustomerService customerService, IShoppingBffService shoppingBffService)
        {
            _customerService = customerService;
            _shoppingBffService = shoppingBffService;
        }

        [HttpGet("delivery-address")]
        public async Task<IActionResult> DeliveryAddress()
        {
            var shoppingCart = await _shoppingBffService.GetShoppingCart();

            if (shoppingCart.Items.Count == 0) 
                return RedirectToAction("Index", "ShoppingCart");

            return View(_shoppingBffService.MapToOrderTransaction(shoppingCart, await _customerService.GetAddress()));
        }

        [HttpGet("payment")]
        public async Task<IActionResult> Payment()
        {
            var shoppingCart = await _shoppingBffService.GetShoppingCart();
            
            if (shoppingCart.Items.Count == 0) 
                return RedirectToAction("Index", "ShoppingCart");

            return View(_shoppingBffService.MapToOrderTransaction(shoppingCart, null));
        }

        [HttpPost("finish-order")]
        public async Task<IActionResult> FinishOrder(OrderTransactionViewModel orderTransactionViewModel)
        {
            if (!ModelState.IsValid) 
                return View("Payment", _shoppingBffService.MapToOrderTransaction(await _shoppingBffService.GetShoppingCart(), null));

            var finishOrderResult = await _shoppingBffService.FinishOrder(orderTransactionViewModel);
            
            if (ResponseHasErrors(finishOrderResult))
            {
                var shoppingCart = await _shoppingBffService.GetShoppingCart();
                
                if (shoppingCart.Items.Count == 0) 
                    return RedirectToAction("Index", "ShoppingCart");
                
                return View("Payment", _shoppingBffService.MapToOrderTransaction(shoppingCart, null));
            }

            return RedirectToAction("OrderFinished");
        }

        [HttpGet("order-finished")]
        public async Task<IActionResult> OrderFinished()
        {
            return View("OrderConfirmation", await _shoppingBffService.GetLastOrder());
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> MyOrders()
        {
            return View(await _shoppingBffService.GetOrdersByCustomerId());
        }
    }
}
