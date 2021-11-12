using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.WebApp.MVC.Models.Catalog;
using NerdStore.WebApp.MVC.Models.ShoppingCart;
using NerdStore.WebApp.MVC.Services.Catalog;
using NerdStore.WebApp.MVC.Services.ShoppingCart;

namespace NerdStore.WebApp.MVC.Controllers
{
    [Route("shopping-cart")]
    public class ShoppingCartController : MainController
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICatalogService _catalogService; 

        public ShoppingCartController(IShoppingCartService shoppingCartService, ICatalogService catalogService)
        {
            _shoppingCartService = shoppingCartService;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _shoppingCartService.GetShoppingCart());
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddItem(ShoppingCartItemViewModel item)
        {
            var product = await _catalogService.GetById(item.ProductId);

            ValidateShoppingCartItem(product, item.Amount);

            if (!IsValidOperation())
                return View("Index", await _shoppingCartService.GetShoppingCart());

            item.Name = product.Name;
            item.Image = product.Image;
            item.Price = product.Price;

            var response = await _shoppingCartService.AddItem(item);

            if (ResponseHasErros(response)) 
                return View("Index", await _shoppingCartService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateItem(Guid productId, int amount)
        {
            var product = await _catalogService.GetById(productId);

            ValidateShoppingCartItem(product, amount);

            if (!IsValidOperation())
                return View("Index", await _shoppingCartService.GetShoppingCart());

            var item = new ShoppingCartItemViewModel 
            { 
                ProductId = productId, 
                Amount = amount 
            };

            var response = await _shoppingCartService.UpdateItem(productId, item);

            if (ResponseHasErros(response)) 
                return View("Index", await _shoppingCartService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost("delete")]
        public async Task<IActionResult> RemoveItem(Guid productId)
        {
            var response = await _shoppingCartService.RemoveItem(productId);

            if (ResponseHasErros(response)) 
                return View("Index", await _shoppingCartService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        private void ValidateShoppingCartItem(ProductViewModel product, int amount)
        {
            if (product == null)
                AddError("Produto não existe");
            if (amount < 1)
                AddError("A quantidade não pode ser menor do que 1");
            if (amount > product.StockAmount)
                AddError("A quantidade informada é maior do que o nosso estoque");
        }
    }
}
