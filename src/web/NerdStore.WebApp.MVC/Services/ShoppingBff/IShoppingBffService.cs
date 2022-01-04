using NerdStore.Core.Communication;
using System;
using System.Threading.Tasks;
using NerdStore.WebApp.MVC.Models.ShoppingCart;
using NerdStore.WebApp.MVC.Models.Order;
using System.Collections.Generic;

namespace NerdStore.WebApp.MVC.Services.ShoppingBff
{
    public interface IShoppingBffService
    {
        // ShoppingCart
        Task<ShoppingCartViewModel> GetShoppingCart();
        Task<int> GetShoppingCartAmount();
        Task<ResponseResult> AddItem(ShoppingCartItemViewModel item);
        Task<ResponseResult> UpdateItem(Guid productId, ShoppingCartItemViewModel item);
        Task<ResponseResult> RemoveItem(Guid produtctId);
        Task<ResponseResult> ApplyVoucher(string voucherCode);

        // Order
        Task<ResponseResult> FinishOrder(OrderTransactionViewModel orderTransaction);
        Task<OrderViewModel> GetLastOrder();
        Task<IEnumerable<OrderViewModel>> GetOrdersByCustomerId();
        OrderTransactionViewModel MapToOrderTransaction(ShoppingCartViewModel shoppingCart, AddressViewModel address);
    }
}
