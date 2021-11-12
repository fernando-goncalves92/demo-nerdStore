using FluentValidation.Results;
using NerdStore.ShoppingCart.API.Entities.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.ShoppingCart.API.Entities
{
    public class ShoppingCart
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal TotalPurchase { get; private set; }
        public List<ShoppingCartItem> Items{ get; private set; } = new List<ShoppingCartItem>();

        public ValidationResult ValidationResult { get; set; }

        public ShoppingCart() { }

        public ShoppingCart(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }

        public void CalculateShoppingCartFinalPrice()
        {
            TotalPurchase = Items.Sum(i => i.CalculateFinalPrice());
        }

        public bool ItemExistsInShoppingCart(ShoppingCartItem item)
        {
            return Items.Any(i => i.ProductId == item.ProductId);
        }

        public ShoppingCartItem GetShoppingCartItemByProductId(Guid productId)
        {
            return Items.FirstOrDefault(i => i.ProductId == productId);
        }

        internal void AddItem(ShoppingCartItem newItem)
        {
            newItem.AssignShoppingCart(Id);

            var exisingItemInShoppingCart = GetShoppingCartItemByProductId(newItem.ProductId);

            if (exisingItemInShoppingCart != null)
            {
                exisingItemInShoppingCart.IncreaseAmount(newItem.Amount);

                newItem = exisingItemInShoppingCart;

                Items.Remove(exisingItemInShoppingCart);
            }

            Items.Add(newItem);

            CalculateShoppingCartFinalPrice();
        }

        public void UpdateShoppingCartItem(ShoppingCartItem updatedItem)
        {
            updatedItem.AssignShoppingCart(Id);

            var exisingItemInShoppingCart = GetShoppingCartItemByProductId(updatedItem.ProductId);

            Items.Remove(exisingItemInShoppingCart);
            Items.Add(updatedItem);

            CalculateShoppingCartFinalPrice();
        }

        public void UpdateShoppingCartItemAmount(ShoppingCartItem item, int amount)
        {
            item.UpdateAmount(amount);

            UpdateShoppingCartItem(item);
        }

        public void RemoveItem(ShoppingCartItem item)
        {
            Items.Remove(GetShoppingCartItemByProductId(item.ProductId));

            CalculateShoppingCartFinalPrice();
        }

        public bool IsValid()
        {
            var errors = Items.SelectMany(i => new ShoppingCartItemValidator().Validate(i).Errors).ToList();
            
            errors.AddRange(new ShoppingCartValidator().Validate(this).Errors);

            ValidationResult = new ValidationResult(errors);

            return errors.Count <= 0;
        }
    }
}
