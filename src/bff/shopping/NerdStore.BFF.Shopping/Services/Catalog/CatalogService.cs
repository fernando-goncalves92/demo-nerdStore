using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NerdStore.BFF.Shopping.Facilities;
using NerdStore.BFF.Shopping.Models;

namespace NerdStore.BFF.Shopping.Services.Catalog
{
    public class CatalogService : ServiceBase, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<UrlAccess> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.CatalogUrl);
        }

        public async Task<ProductDto> GetProductById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/v1/catalog/products/{id}");

            IsSuccessResponseStatusCode(response);

            return await GetResponse<ProductDto>(response);
        }
    }
}
