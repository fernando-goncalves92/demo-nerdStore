using FluentValidation;
using System;

namespace NerdStore.ShoppingCart.API.Entities.Validators
{
    public class ShoppingCartValidator : AbstractValidator<ShoppingCart>
    {
        public ShoppingCartValidator()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente não reconhecido");

            RuleFor(c => c.Items.Count)
                .GreaterThan(0)
                .WithMessage("O carrinho não possui itens adicionados");

            RuleFor(c => c.TotalPurchase)
                .GreaterThan(0)
                .WithMessage("O valor total do carrinho precisa ser maior que 0");
        }
    }
}
