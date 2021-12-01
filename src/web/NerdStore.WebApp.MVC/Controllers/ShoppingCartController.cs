using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NerdStore.WebApp.MVC.Models.ShoppingCart;
using NerdStore.WebApp.MVC.Services.ShoppingBff;

namespace NerdStore.WebApp.MVC.Controllers
{
    [Route("shopping-cart")]
    //[Authorize]
    public class ShoppingCartController : MainController
    {
        private readonly IShoppingBffService _shoppingBffService;

        public ShoppingCartController(IShoppingBffService shoppingBffService)
        {
            _shoppingBffService = shoppingBffService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _shoppingBffService.GetShoppingCart());
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddItem(ShoppingCartItemViewModel item)
        {
            var response = await _shoppingBffService.AddItem(item);

            if (ResponseHasErrors(response))
                return View("Index", await _shoppingBffService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateItem(Guid productId, int amount)
        {
            var item = new ShoppingCartItemViewModel { ProductId = productId, Amount = amount };
            var response = await _shoppingBffService.UpdateItem(productId, item);

            if (ResponseHasErrors(response))
                return View("Index", await _shoppingBffService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost("delete")]
        public async Task<IActionResult> RemoveItem(Guid productId)
        {
            var response = await _shoppingBffService.RemoveItem(productId);

            if (ResponseHasErrors(response)) 
                return View("Index", await _shoppingBffService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost("apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(string voucherCode)
        {
            var response = await _shoppingBffService.ApplyVoucher(voucherCode);

            if (ResponseHasErrors(response)) 
                return View("Index", await _shoppingBffService.GetShoppingCart());

            return RedirectToAction("Index");
        }
    }
}
