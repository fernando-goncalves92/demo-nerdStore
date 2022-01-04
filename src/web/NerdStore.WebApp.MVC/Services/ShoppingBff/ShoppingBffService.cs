using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NerdStore.WebApp.MVC.Facilities;
using NerdStore.Core.Communication;
using NerdStore.WebApp.MVC.Models.ShoppingCart;
using NerdStore.WebApp.MVC.Models.Order;
using System.Collections.Generic;

namespace NerdStore.WebApp.MVC.Services.ShoppingBff
{
    public class ShoppingBffService : ServiceBase, IShoppingBffService
    {
        private readonly HttpClient _httpClient;

        public ShoppingBffService(HttpClient httpClient, IOptions<UrlAccess> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.ShoppingBffUrl);
        }

        public async Task<ShoppingCartViewModel> GetShoppingCart()
        {
            var response = await _httpClient.GetAsync("/api/v1/shopping/cart");

            IsSuccessResponseStatusCode(response);

            return await GetResponse<ShoppingCartViewModel>(response);
        }

        public async Task<int> GetShoppingCartAmount()
        {
            var response = await _httpClient.GetAsync("/api/v1/shopping/cart/items/amount");

            IsSuccessResponseStatusCode(response);

            return await GetResponse<int>(response);
        }

        public async Task<ResponseResult> AddItem(ShoppingCartItemViewModel item)
        {
            var itemContent = ConvertToStringContent(item);
            var response = await _httpClient.PostAsync("/api/v1/shopping/cart/items", itemContent);

            if (!IsSuccessResponseStatusCode(response))
                return await GetResponse<ResponseResult>(response);

            return Ok();
        }

        public async Task<ResponseResult> UpdateItem(Guid productId, ShoppingCartItemViewModel item)
        {
            var itemContent = ConvertToStringContent(item);
            var response = await _httpClient.PutAsync($"/api/v1/shopping/cart/items/{productId}", itemContent);

            if (!IsSuccessResponseStatusCode(response))
                return await GetResponse<ResponseResult>(response);

            return Ok();
        }

        public async Task<ResponseResult> RemoveItem(Guid produtctId)
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/shopping/cart/items/{produtctId}");

            if (!IsSuccessResponseStatusCode(response))
                return await GetResponse<ResponseResult>(response);

            return Ok();
        }

        public async Task<ResponseResult> ApplyVoucher(string voucherCode)
        {
            var response = await _httpClient.PostAsync("/api/v1/shopping/cart/apply-voucher/", ConvertToStringContent(voucherCode));

            if (!IsSuccessResponseStatusCode(response)) 
                return await GetResponse<ResponseResult>(response);

            return Ok();
        }

        public async Task<ResponseResult> FinishOrder(OrderTransactionViewModel orderTransaction)
        {
            var orderContent = ConvertToStringContent(orderTransaction);
            var response = await _httpClient.PostAsync("/api/v1/shopping/order/", orderContent);

            if (!IsSuccessResponseStatusCode(response)) 
                return await GetResponse<ResponseResult>(response);

            return Ok();
        }

        public async Task<OrderViewModel> GetLastOrder()
        {
            var response = await _httpClient.GetAsync("/api/v1/shopping/order/last/");

            IsSuccessResponseStatusCode(response);

            return await GetResponse<OrderViewModel>(response);
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrdersByCustomerId()
        {
            var response = await _httpClient.GetAsync("/api/v1/shopping/order/customer-list");

            IsSuccessResponseStatusCode(response);

            return await GetResponse<IEnumerable<OrderViewModel>>(response);
        }

        public OrderTransactionViewModel MapToOrderTransaction(ShoppingCartViewModel shoppingCart, AddressViewModel address)
        {
            var orderTransaction = new OrderTransactionViewModel
            {
                TotalPurchase = shoppingCart.TotalPurchase,
                ShoppingCartItems = shoppingCart.Items,
                Discount = shoppingCart.Discount,
                VoucherUsed = shoppingCart.VoucherUsed,
                VoucherCode = shoppingCart.Voucher?.Code
            };

            if (address != null)
            {
                orderTransaction.Address = new AddressViewModel
                {
                    Street = address.Street,
                    Number = address.Number,
                    District = address.District,
                    ZipCode = address.ZipCode,
                    Complement = address.Complement,
                    City = address.City,
                    State = address.State
                };
            }

            return orderTransaction;
        }
    }
}
