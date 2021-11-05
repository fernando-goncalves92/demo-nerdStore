using System;

namespace NerdStore.ShoppingCart.API.Entities
{
    public class ShoppingCartItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Amount { get; private set; }
        public decimal Price { get; private set; }
        public string Image { get; private set; }
        public Guid ShoppingCartId { get; private set; }
        public ShoppingCart ShoppingCart { get; private set; }

        protected ShoppingCartItem() { }
        
        public ShoppingCartItem(Guid shoppingCartId)
        {
            Id = Guid.NewGuid();
            ShoppingCartId = shoppingCartId;
        }
    }
}
