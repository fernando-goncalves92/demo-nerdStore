using FluentValidation;
using NerdStore.Core.Messages;
using NerdStore.Order.API.Application.DTO;
using System;
using System.Collections.Generic;

namespace NerdStore.Order.API.Application.Commands
{
    public class AddOrderCommand : Command
    {   
        public Guid CustomerId { get; set; }
        public decimal TotalPurchase { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }                
        public string VoucherCode { get; set; }
        public bool VoucherUsed { get; set; }
        public decimal Discount { get; set; }
        public AddressDto Address { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardName { get; set; }
        public string CreditCardExpirationDate { get; set; }
        public string CreditCardCvv { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new AddOrderValidation().Validate(this);

            return ValidationResult.IsValid;
        }

        public class AddOrderValidation : AbstractValidator<AddOrderCommand>
        {
            public AddOrderValidation()
            {
                RuleFor(c => c.CustomerId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("O Id do cliente é inválido");

                RuleFor(c => c.OrderItems.Count)
                    .GreaterThan(0)
                    .WithMessage("O pedido precisa ter no mínimo 1 item");

                RuleFor(c => c.TotalPurchase)
                    .GreaterThan(0)
                    .WithMessage("O valor do pedido é inválido");

                RuleFor(c => c.CreditCardNumber)
                    .CreditCard()
                    .WithMessage("O número de cartão é inválido");

                RuleFor(c => c.CreditCardName)
                    .NotNull()
                    .WithMessage("O nome do portador do cartão é obrigatório");

                RuleFor(c => c.CreditCardCvv.Length)
                    .GreaterThan(2)
                    .LessThan(5)
                    .WithMessage("O CVV do cartão precisa ter 3 ou 4 números");

                RuleFor(c => c.CreditCardExpirationDate)
                    .NotNull()
                    .WithMessage("A data de expiração do cartão é obrigatória");
            }
        }
    }
}
