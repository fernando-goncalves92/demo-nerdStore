using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NerdStore.Core.Communication;

namespace NerdStore.BFF.Shopping.Services
{
    public abstract class ServiceBase
    {
        protected bool IsSuccessResponseStatusCode(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
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
