using NerdStore.Order.API.Application.DTO;
using System.Threading.Tasks;

namespace NerdStore.Order.API.Application.Queries.Voucher
{
    public interface IVoucherQuery
    {
        Task<VoucherDto> GetVoucherByCode(string code);
    }
}
