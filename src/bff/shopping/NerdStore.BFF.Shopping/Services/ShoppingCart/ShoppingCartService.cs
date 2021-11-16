using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using NerdStore.BFF.Shopping.Facilities;
using NerdStore.BFF.Shopping.Services;
using NerdStore.BFF.Shopping.Services.ShoppingCart;

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
    }
}
