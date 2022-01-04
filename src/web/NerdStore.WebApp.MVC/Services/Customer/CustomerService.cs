using Microsoft.Extensions.Options;
using NerdStore.Core.Communication;
using NerdStore.WebApp.MVC.Facilities;
using NerdStore.WebApp.MVC.Models.Order;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Services.Customer
{
    public class CustomerService : ServiceBase, ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient, IOptions<UrlAccess> urlAccess)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(urlAccess.Value.CustomerUrl);
        }

        public async Task<ResponseResult> AddAddress(AddressViewModel address)
        {
            var response = await _httpClient.PostAsync("api/v1/customer/address/", ConvertToStringContent(address));

            if (!IsSuccessResponseStatusCode(response)) 
                return await GetResponse<ResponseResult>(response);

            return Ok();
        }

        public async Task<AddressViewModel> GetAddress()
        {
            var response = await _httpClient.GetAsync("api/v1/customer/address/");

            if (response.StatusCode == HttpStatusCode.NotFound) 
                return null;

            IsSuccessResponseStatusCode(response);

            return await GetResponse<AddressViewModel>(response);
        }
    }
}
