using NetDevPack.Specification;
using System;
using System.Linq.Expressions;

namespace NerdStore.Order.Domain.Voucher
{
    public class VoucherSpecificationIsActive : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression()
        {
            return voucher => voucher.IsActive && !voucher.IsUsed;
        }
    }
}
