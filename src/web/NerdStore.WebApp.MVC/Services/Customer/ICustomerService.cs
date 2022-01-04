using NerdStore.Core.Communication;
using NerdStore.WebApp.MVC.Models.Order;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Services.Customer
{
    public interface ICustomerService
    {
        Task<AddressViewModel> GetAddress();
        Task<ResponseResult> AddAddress(AddressViewModel address);
    }
}
