using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using NerdStore.BFF.Shopping.Facilities;

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
    }
}
