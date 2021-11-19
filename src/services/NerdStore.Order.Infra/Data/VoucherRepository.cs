using System;
using System.Threading.Tasks;
using NerdStore.Core.Data.Interfaces;
using NerdStore.Order.Domain.Voucher;
using Microsoft.EntityFrameworkCore;

namespace NerdStore.Order.Infra.Data
{
    public class VoucherRepository : IVoucherRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        private readonly OrderDbContext _context;

        public VoucherRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Voucher> GetVoucherByCode(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(v => v.Code == code);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
