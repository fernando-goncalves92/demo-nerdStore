using Microsoft.AspNetCore.Mvc;
using NerdStore.BFF.Shopping.Models;
using NerdStore.BFF.Shopping.Services.Catalog;
using NerdStore.BFF.Shopping.Services.ShoppingCart;
using NerdStore.WebAPI.Core.Controllers;
using NerdStore.WebAPI.Core.Facilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.BFF.Shopping.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ShoppingController : MainController
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICatalogService _catalogService;

        public ShoppingController(IAspNetUser aspNetUser, IShoppingCartService shoppingCartService, ICatalogService catalogService)
        {
            _aspNetUser = aspNetUser;
            _shoppingCartService = shoppingCartService;
            _catalogService = catalogService;
        }

        [HttpGet("cart")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _shoppingCartService.GetShoppingCart());
        }

        [HttpGet("cart/items/amount")]
        public async Task<int> GetTotalItemsInShoppingCart()
        {
            var shoppingCart = await _shoppingCartService.GetShoppingCart();

            return shoppingCart?.Items.Sum(i => i.Amount) ?? 0;
        }

        [HttpPost("cart/items")]
        public async Task<IActionResult> AddShoppingCartItem(ShoppingCartItemDto item)
        {
            var product = await _catalogService.GetProductById(item.ProductId);

            await ValidateShoppingCartItem(product, item.Amount, true);

            if (!IsValidOperation())
            {
                return CustomResponse();
            }

            item.Name = product.Name;
            item.Price = product.Price;
            item.Image = product.Image;

            return CustomResponse(await _shoppingCartService.AddItem(item));
        }

        [HttpPut("cart/items/{productId:guid}")]
        public async Task<IActionResult> UpdateShoppingCartItem(Guid productId, ShoppingCartItemDto item)
        {
            var product = await _catalogService.GetProductById(productId);

            await ValidateShoppingCartItem(product, item.Amount, true);

            if (!IsValidOperation())
            {
                return CustomResponse();
            }

            return CustomResponse(await _shoppingCartService.UpdateItem(productId, item));
        }

        [HttpDelete("cart/items/{productId:guid}")]
        public async Task<IActionResult> RemoveShoppingCartItem(Guid productId)
        {
            var product = await _catalogService.GetProductById(productId);

            if (product == null)
            {
                AddError("Produto inexistente!");

                return CustomResponse();
            }

            return CustomResponse(await _shoppingCartService.RemoveItem(productId));
        }

        private async Task ValidateShoppingCartItem(ProductDto product, int amount, bool addProduct = false)
        {
            if (product == null)
                AddError("O produto informado não existe");            
            if (amount < 1)
                AddError($"Você não adicionou unidades para o produto \"{product.Name}\"");

            var shoppingCart = await _shoppingCartService.GetShoppingCart();
            var item = shoppingCart.Items.FirstOrDefault(p => p.ProductId == product.Id);

            if (amount > product.StockAmount)            
                AddError($"O produto \"{product.Name}\" possui {product.StockAmount} unidades em estoque, você selecionou {amount}");
            if (item != null && addProduct && item.Amount + amount > product.StockAmount)            
                AddError($"O produto \"{product.Name}\" possui {product.StockAmount} unidades em estoque, você selecionou {amount}");
        }
    }
}
