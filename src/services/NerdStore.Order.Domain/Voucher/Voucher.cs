using System;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Order.Domain.Voucher
{
    public class Voucher : Entity, IAggregateRoot
    {
        public string Code { get; private set; }
        public decimal? DiscountPercentage { get; private set; }
        public decimal? DiscountAmount { get; private set; }
        public int AvailableAmount { get; private set; }
        public DiscountType DiscountType { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? UsedDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsUsed { get; private set; }

        public bool IsValid()
        {
            return new VoucherSpecificationAvailableAmount()
                .And(new VoucherSpecificationExpirationDate())
                .And(new VoucherSpecificationIsActive())
                .IsSatisfiedBy(this);
        }

        public void MarkAsUsed()
        {
            IsActive = false;
            IsUsed = true;
            AvailableAmount = 0;
            UsedDate = DateTime.Now;
        }

        public void DebitAvailableAmount()
        {
            AvailableAmount -= 1;
            
            if (AvailableAmount >= 1) 
                return;

            MarkAsUsed();
        }
    }
}
