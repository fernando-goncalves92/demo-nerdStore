using System;
using System.Linq;
using System.Collections.Generic;
using NerdStore.Core.DomainObjects;
using NerdStore.Order.Domain.Voucher;

namespace NerdStore.Order.Domain.Order
{
    public class Order : Entity, IAggregateRoot
    {
        protected Order() { }

        public Order(Guid customerId, 
            decimal totalPurchase, 
            List<OrderItem> orderItems,
            bool voucherUsed = false, 
            decimal discount = 0, 
            Guid? voucherId = null)
        {
            CustomerId = customerId;
            TotalPurchase = totalPurchase;
            Discount = discount;
            VoucherUsed = voucherUsed;
            VoucherId = voucherId;

            _orderItems = orderItems;
        }

        public int Code { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalPurchase { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public Address Address { get; private set; }
        public Voucher.Voucher Voucher { get; private set; }
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public void AuthorizeOrder()
        {
            OrderStatus = OrderStatus.Authorized;
        }
        public void CancelOrder()
        {
            OrderStatus = OrderStatus.Canceled;
        }

        public void FinishOrder()
        {
            OrderStatus = OrderStatus.Payed;
        }

        public void AssignVoucher(Voucher.Voucher voucher)
        {
            Voucher = voucher;
            VoucherUsed = true;
            VoucherId = voucher.Id;
        }

        public void AssignAddress(Address address)
        {
            Address = address;
        }

        public void CalculateShoppingCartFinalPrice()
        {
            TotalPurchase = _orderItems.Sum(i => i.CalculatePrice());

            CalculateDiscount();
        }

        private void CalculateDiscount()
        {
            if (VoucherUsed)
            {
                decimal discount = 0;
                decimal totalPurchase = TotalPurchase;

                if (Voucher.DiscountType == DiscountType.Percentage)
                {
                    if (Voucher.DiscountPercentage.HasValue)
                    {
                        discount = (totalPurchase * Voucher.DiscountPercentage.Value) / 100;

                        totalPurchase -= discount;
                    }
                }
                else
                {
                    if (Voucher.DiscountAmount.HasValue)
                    {
                        discount = Voucher.DiscountAmount.Value;

                        totalPurchase -= discount;
                    }
                }

                TotalPurchase = totalPurchase < 0 ? 0 : totalPurchase;
                Discount = discount;
            }
        }
    }
}
