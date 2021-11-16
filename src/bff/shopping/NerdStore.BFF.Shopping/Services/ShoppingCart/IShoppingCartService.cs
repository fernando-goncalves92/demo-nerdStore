using NerdStore.BFF.Shopping.Models;
using NerdStore.Core.Communication;
using System;
using System.Threading.Tasks;

namespace NerdStore.BFF.Shopping.Services.ShoppingCart
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetShoppingCart();
        Task<ResponseResult> AddItem(ShoppingCartItemDto item);
        Task<ResponseResult> UpdateItem(Guid productId, ShoppingCartItemDto item);
        Task<ResponseResult> RemoveItem(Guid productId);
    }
}
