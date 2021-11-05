using System;
using System.Collections.Generic;

namespace NerdStore.ShoppingCart.API.Entities
{
    public class ShoppingCart
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal TotalPurchase { get; private set; }
        public List<ShoppingCartItem> Items{ get; private set; } = new List<ShoppingCartItem>();

        protected ShoppingCart() { }

        public ShoppingCart(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }
    }
}
