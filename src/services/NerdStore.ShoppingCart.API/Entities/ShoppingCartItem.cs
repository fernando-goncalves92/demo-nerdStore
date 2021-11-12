using System;
using System.Text.Json.Serialization;
using NerdStore.ShoppingCart.API.Entities.Validators;

namespace NerdStore.ShoppingCart.API.Entities
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public Guid ShoppingCartId { get; private set; }

        [JsonIgnore]
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
