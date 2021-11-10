using System;
using NerdStore.ShoppingCart.API.Entities.Validators;

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

        public void AssignShoppingCart(Guid shoppingCartId)
        {
            ShoppingCartId = shoppingCartId;
        }

        public decimal CalculateFinalPrice()
        {
            return Amount * Price;
        }

        public void IncreaseAmount(int amount)
        {
            Amount += amount;
        }

        public void UpdateAmount(int amount)
        {
            Amount = amount;
        }

        public bool IsValid()
        {
            return new ShoppingCartItemValidator().Validate(this).IsValid;
        }
    }
}
