using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NerdStore.WebApp.MVC.Facilities;
using NerdStore.WebApp.MVC.Models.User;
using NerdStore.Core.Communication;
using NerdStore.WebApp.MVC.Models.ShoppingCart;

namespace NerdStore.WebApp.MVC.Services.ShoppingCart
{
    public class ShoppingCartService : ServiceBase, IShoppingCartService
    {
        private readonly HttpClient _httpClient;

        public ShoppingCartService(HttpClient httpClient, IOptions<UrlAccess> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.ShoppingCartUrl);
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
    }
}
