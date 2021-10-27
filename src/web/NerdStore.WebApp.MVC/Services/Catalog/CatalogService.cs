using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NerdStore.WebApp.MVC.Facilities;
using NerdStore.WebApp.MVC.Models.Catalog;
using System.Collections.Generic;

namespace NerdStore.WebApp.MVC.Services.Catalog
{
    public class CatalogService : ServiceBase, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<UrlAccess> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.CatalogUrl);
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {   
            var response = await _httpClient.GetAsync("/api/v1/catalog/products");

            VerifyResponseErrors(response);

            return await GetResponse<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/catalog/products/{id}");

            VerifyResponseErrors(response);

            return await GetResponse<ProductViewModel>(response);
        }
    }
}
