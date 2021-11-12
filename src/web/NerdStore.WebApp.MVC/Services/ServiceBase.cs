using NerdStore.Core.Communication;
using NerdStore.WebApp.MVC.Exceptions;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Services
{
    public abstract class ServiceBase
    {
        protected bool IsSuccessResponseStatusCode(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500: throw new CustomHttpResponseException(response.StatusCode);
                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            
            return true;
        }

        protected StringContent ConvertToStringContent(object obj)
        {
            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
        }

        protected async Task<T> GetResponse<T>(HttpResponseMessage response)
        {
            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        protected ResponseResult Ok()
        {
            return new ResponseResult();
        }
    }
}
