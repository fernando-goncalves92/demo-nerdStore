﻿using NerdStore.Core.Communication;
using System;
using System.Threading.Tasks;
using NerdStore.WebApp.MVC.Models.ShoppingCart;

namespace NerdStore.WebApp.MVC.Services.ShoppingCart
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartViewModel> GetShoppingCart();
        Task<int> GetShoppingCartAmount();
        Task<ResponseResult> AddItem(ShoppingCartItemViewModel item);
        Task<ResponseResult> UpdateItem(Guid productId, ShoppingCartItemViewModel item);
        Task<ResponseResult> RemoveItem(Guid produtctId);
    }
}
