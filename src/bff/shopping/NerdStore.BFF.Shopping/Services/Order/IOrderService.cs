using NerdStore.BFF.Shopping.Models;
using System.Threading.Tasks;

namespace NerdStore.BFF.Shopping.Services.Catalog
{
    public interface IOrderService
    {
        Task<VoucherDto> GetVoucherByCode(string code);
    }
}
