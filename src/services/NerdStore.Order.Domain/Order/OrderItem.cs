using System;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Order.Domain.Order
{
    public class OrderItem : Entity
    {
        protected OrderItem() { }

        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Amount { get; private set; }
        public decimal UnitPrice { get; private set; }
        public string ProductImage { get; set; }
        public Order Order { get; set; }

        public OrderItem(
            Guid productId, 
            string productName, 
            int amount,
            decimal unitPrice, 
            string productImage = null)
        {
            ProductId = productId;
            ProductName = productName;
            Amount = amount;
            UnitPrice = unitPrice;
            ProductImage = productImage;
        }

        internal decimal CalculatePrice()
        {
            return Amount * UnitPrice;
        }
    }
}
