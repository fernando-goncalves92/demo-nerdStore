using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NerdStore.BFF.Shopping.Facilities;
using NerdStore.BFF.Shopping.Models;

namespace NerdStore.BFF.Shopping.Services.Catalog
{
    public class OrderService : ServiceBase, IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient, IOptions<UrlAccess> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.OrderUrl);
        }

        public async Task<VoucherDto> GetVoucherByCode(string code)
        {
            var response = await _httpClient.GetAsync($"/api/v1/voucher/{code}");

            if (response.StatusCode == HttpStatusCode.NotFound) 
                return null;

            IsSuccessResponseStatusCode(response);
            
            return await GetResponse<VoucherDto>(response);
        }
    }
}
