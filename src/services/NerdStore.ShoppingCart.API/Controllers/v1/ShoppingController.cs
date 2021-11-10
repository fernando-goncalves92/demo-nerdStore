﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NerdStore.ShoppingCart.API.Data;
using NerdStore.WebAPI.Core.Controllers;
using NerdStore.WebAPI.Core.Facilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.ShoppingCart.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ShoppingController : MainController
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly ShoppingContext _shoppingContext;

        public ShoppingController(IAspNetUser aspNetUser, ShoppingContext shoppingContext)
        {
            _aspNetUser = aspNetUser;
            _shoppingContext = shoppingContext;
        }

        [HttpGet("cart")]
        public async Task<Entities.ShoppingCart> GetShoppingCart()
        {
            return await GetShoppingCartCustomer() ?? new Entities.ShoppingCart();
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddShoppingCartItem(Entities.ShoppingCartItem item)
        {
            var shoppingCart = await GetShoppingCartCustomer();

            if (shoppingCart == null)
            {
                shoppingCart = new Entities.ShoppingCart(_aspNetUser.GetUserId());
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

            if (await _shoppingContext.SaveChangesAsync() <= 0)
                AddError("Não foi possível salvar as alterações!");

            return CustomResponse();
        }

        [HttpPut("cart/{productId:guid}")]
        public async Task<IActionResult> UpdateShoppingCartItem(Guid productId, Entities.ShoppingCartItem shoppingCartItemUpdated)
        {
            var shoppingCart = await GetShoppingCartCustomer();
            var shoppingCartItem = await GetItemFromShoppingCart(productId, shoppingCart, shoppingCartItemUpdated);
            
            if (shoppingCartItem == null) 
                return CustomResponse();

            shoppingCart.UpdateShoppingCartItemAmount(shoppingCartItem, shoppingCartItem.Amount);

            if (!IsShoppingCartValid(shoppingCart))
                return CustomResponse();

            _shoppingContext.ShoppingCartItems.Update(shoppingCartItem);
            _shoppingContext.ShoppingCarts.Update(shoppingCart);

            if (await _shoppingContext.SaveChangesAsync() <= 0)
                AddError("Não foi possível salvar as alterações!");

            return CustomResponse();
        }

        [HttpDelete("cart/{productId:guid}")]
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

            if (await _shoppingContext.SaveChangesAsync() <= 0)
                AddError("Não foi possível salvar as alterações!");

            return CustomResponse();
        }

        private async Task<Entities.ShoppingCart> GetShoppingCartCustomer()
        {
            return await _shoppingContext
                .ShoppingCarts
                .Include(s => s.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == _aspNetUser.GetUserId());
        }

        private async Task<Entities.ShoppingCartItem> GetItemFromShoppingCart(Guid productId, Entities.ShoppingCart shoppingCart, Entities.ShoppingCartItem item = null)
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
    }
}
