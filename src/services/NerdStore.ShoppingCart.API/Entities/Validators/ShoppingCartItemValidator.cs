using FluentValidation;
using System;

namespace NerdStore.ShoppingCart.API.Entities.Validators
{
    public class ShoppingCartItemValidator : AbstractValidator<ShoppingCartItem>
    {
        public ShoppingCartItemValidator()
        {
            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("O id do produto é inválido");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.Amount)
                .GreaterThan(0)
                .WithMessage(item => $"A quantidade miníma para o produto \"{item.Name}\" é 1");

            RuleFor(c => c.Amount)
                .LessThanOrEqualTo(5)
                .WithMessage(item => $"A quantidade máxima para o produto \"{item.Name}\" é 5");

            RuleFor(c => c.Price)
                .GreaterThan(0)
                .WithMessage(item => $"O valor do produto \"{item.Name}\" deve ser maior do que 0");
        }
    }
}
