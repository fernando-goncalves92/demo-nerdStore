using NerdStore.Order.API.Application.DTO;
using NerdStore.Order.Domain.Voucher;
using System.Threading.Tasks;

namespace NerdStore.Order.API.Application.Queries.Voucher
{
    public class VoucherQuery : IVoucherQuery
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherQuery(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<VoucherDto> GetVoucherByCode(string code)
        {
            var voucher = await _voucherRepository.GetVoucherByCode(code);

            if (voucher == null || !voucher.IsValid()) 
                return null;

            return new VoucherDto
            {
                Code = voucher.Code,
                DiscountType = (int)voucher.DiscountType,
                DiscountPercentage = voucher.DiscountPercentage,
                DiscountAmount = voucher.DiscountAmount
            };
        }
    }
}
