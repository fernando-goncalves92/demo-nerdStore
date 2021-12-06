using System.Threading.Tasks;
using NerdStore.Core.Data.Interfaces;

namespace NerdStore.Order.Domain.Voucher
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher> GetVoucherByCode(string code);
        void Update(Voucher voucher);
    }
}
