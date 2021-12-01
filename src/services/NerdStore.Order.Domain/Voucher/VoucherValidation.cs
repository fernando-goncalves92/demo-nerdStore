using NetDevPack.Specification;

namespace NerdStore.Order.Domain.Voucher
{
    public class VoucherValidation : SpecValidator<Voucher>
    {
        public VoucherValidation()
        {
            Add("expirationDateSpecification", new Rule<Voucher>(new VoucherSpecificationExpirationDate(), "Este voucher expirou"));            
            Add("availableAmountSpecification", new Rule<Voucher>(new VoucherSpecificationAvailableAmount(), "Este voucher já foi utilizado"));            
            Add("activeSpecification", new Rule<Voucher>(new VoucherSpecificationIsActive(), "Este voucher não está mais ativo"));
        }
    }
}
