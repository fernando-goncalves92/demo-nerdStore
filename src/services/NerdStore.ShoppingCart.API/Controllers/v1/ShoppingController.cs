using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NerdStore.ShoppingCart.API.Data;
using NerdStore.ShoppingCart.API.Entities;
using NerdStore.WebAPI.Core.Controllers;
using NerdStore.WebAPI.Core.Facilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.ShoppingCart.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ShoppingController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly ShoppingContext _shoppingContext;

        public ShoppingController(IAspNetUser user, ShoppingContext shoppingContext)
        {
            _user = user;
            _shoppingContext = shoppingContext;
        }

        [HttpGet("cart")]
        public async Task<Entities.ShoppingCart> GetShoppingCart()
        {
            return await GetShoppingCartCustomer() ?? new Entities.ShoppingCart();
        }

        [HttpGet("cart/items/amount")]
        public async Task<int> GetTotalItemsInShoppingCart()
        {
            var shoppingCart = await GetShoppingCartCustomer() ?? new Entities.ShoppingCart();

            return shoppingCart.Items.Count;
        }

        [HttpPost("cart/items")]
        public async Task<IActionResult> AddShoppingCartItem(ShoppingCartItem item)
        {
            var shoppingCart = await GetShoppingCartCustomer();

            if (shoppingCart == null)
            {
                shoppingCart = new Entities.ShoppingCart(_user.GetUserId());
                shoppingCart.AddItem(item);

                if (!IsShoppingCartValid(shoppingCart))
                    return CustomResponse();

                _shoppingContext.ShoppingCarts.Add(shoppingCart);
            }
            else
            {
                var existingItemInShoppingCart = shoppingCart.ItemExistsInShoppingCart(item);

                shoppingCart.AddItem(item);

                if (!IsShoppingCartValid(shoppingCart))
                    return CustomResponse();

                if (existingItemInShoppingCart)                
                    _shoppingContext.ShoppingCartItems.Update(shoppingCart.GetShoppingCartItemByProductId(item.ProductId));                
                else                
                    _shoppingContext.ShoppingCartItems.Add(item);

                _shoppingContext.ShoppingCarts.Update(shoppingCart);
            }

            if (!IsValidOperation())
                return CustomResponse();

            await PersistData();

            return CustomResponse();
        }

        [HttpPut("cart/items/{productId:guid}")]
        public async Task<IActionResult> UpdateShoppingCartItem(Guid productId, ShoppingCartItem shoppingCartItemUpdated)
        {
            var shoppingCart = await GetShoppingCartCustomer();
            var shoppingCartItem = await GetItemFromShoppingCart(productId, shoppingCart, shoppingCartItemUpdated);
            
            if (shoppingCartItem == null) 
                return CustomResponse();

            shoppingCart.UpdateShoppingCartItemAmount(shoppingCartItem, shoppingCartItemUpdated.Amount);

            if (!IsShoppingCartValid(shoppingCart))
                return CustomResponse();

            _shoppingContext.ShoppingCartItems.Update(shoppingCartItem);
            _shoppingContext.ShoppingCarts.Update(shoppingCart);

            await PersistData();

            return CustomResponse();
        }

        [HttpDelete("cart/items/{productId:guid}")]
        public async Task<IActionResult> RemoveShoppingCartItem(Guid productId)
        {
            var shoppingCart = await GetShoppingCartCustomer();
            var shoppingCartItem = await GetItemFromShoppingCart(productId, shoppingCart);

            if (shoppingCartItem == null) 
                return CustomResponse();

            if (!IsShoppingCartValid(shoppingCart))
                return CustomResponse();

            shoppingCart.RemoveItem(shoppingCartItem);

            _shoppingContext.ShoppingCartItems.Remove(shoppingCartItem);
            _shoppingContext.ShoppingCarts.Update(shoppingCart);

            await PersistData();

            return CustomResponse();
        }

        [HttpPost]
        [Route("cart/apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(Voucher voucher)
        {
            var shoppingCart = await GetShoppingCartCustomer();

            shoppingCart.ApplyVoucher(voucher);

            _shoppingContext.ShoppingCarts.Update(shoppingCart);

            await PersistData();

            return CustomResponse();
        }

        private async Task<Entities.ShoppingCart> GetShoppingCartCustomer()
        {
            return await _shoppingContext
                .ShoppingCarts
                .Include(s => s.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == _user.GetUserId());
        }

        private async Task<ShoppingCartItem> GetItemFromShoppingCart(Guid productId, Entities.ShoppingCart shoppingCart, ShoppingCartItem item = null)
        {
            if (shoppingCart == null)
            {
                AddError("Carrinho não encontrado");

                return null;
            }

            if (item != null && productId != item.ProductId)
            {
                AddError("O item não corresponde ao informado");

                return null;
            }

            var shoppingCartItem = await _shoppingContext
                .ShoppingCartItems
                .FirstOrDefaultAsync(i => i.ShoppingCartId == shoppingCart.Id && i.ProductId == productId);

            if (shoppingCartItem == null || !shoppingCart.ItemExistsInShoppingCart(shoppingCartItem))
            {
                AddError("O item não está no carrinho");

                return null;
            }

            return shoppingCartItem;
        }

        private bool IsShoppingCartValid(Entities.ShoppingCart shoppingCart)
        {
            if (shoppingCart.IsValid()) 
                return true;

            shoppingCart
                .ValidationResult
                .Errors
                .ToList()
                .ForEach(e => AddError(e.ErrorMessage));
            
            return false;
        }

        private async Task PersistData()
        {
            if (await _shoppingContext.SaveChangesAsync() <= 0)
                AddError("Não foi possível salvar as alterações!");
        }
    }
}
