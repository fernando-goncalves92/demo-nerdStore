using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NerdStore.BFF.Shopping.Facilities;
using NerdStore.BFF.Shopping.Models;
using NerdStore.Core.Communication;

namespace NerdStore.BFF.Shopping.Services.ShoppingCart
{
    public class ShoppingCartService : ServiceBase, IShoppingCartService
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartService(HttpClient httpClient, IOptions<UrlAccess> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.ShoppingCartUrl);
        }

        public async Task<ShoppingCartDto> GetShoppingCart()
        {
            var response = await _httpClient.GetAsync("api/v1/shopping/cart");

            IsSuccessResponseStatusCode(response);

            return await GetResponse<ShoppingCartDto>(response);
        }

        public async Task<ResponseResult> AddItem(ShoppingCartItemDto item)
        {
            var response = await _httpClient.PostAsync("api/v1/shopping/cart/items", ConvertToStringContent(item));

            if (!IsSuccessResponseStatusCode(response))
            {
                return await GetResponse<ResponseResult>(response);
            }

            return Ok();
        }

        public async Task<ResponseResult> UpdateItem(Guid productId, ShoppingCartItemDto item)
        {
            var response = await _httpClient.PutAsync($"api/v1/shopping/cart/items/{item.ProductId}", ConvertToStringContent(item));

            if (!IsSuccessResponseStatusCode(response))
            {
                return await GetResponse<ResponseResult>(response);
            }

            return Ok();
        }

        public async Task<ResponseResult> RemoveItem(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"api/v1/shopping/cart/items/{productId}");

            if (!IsSuccessResponseStatusCode(response))
            {
                return await GetResponse<ResponseResult>(response);
            }

            return Ok();
        }

        public async Task<ResponseResult> ApplyVoucher(VoucherDto voucher)
        {
            var response = await _httpClient.PostAsync("/api/v1/shopping/cart/apply-voucher", ConvertToStringContent(voucher));

            if (!IsSuccessResponseStatusCode(response)) 
                return await GetResponse<ResponseResult>(response);

            return Ok();
        }
    }
}
