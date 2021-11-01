using FluentValidation;
using NerdStore.Customer.API.ValueObjects;
using System;

namespace NerdStore.Customer.API.Commands.Validators
{
    public class AddCustomerValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O nome do cliente não foi informado");

            RuleFor(c => c.Cpf)
                .Must(Cpf.IsValid)
                .WithMessage("O CPF informado não é válido.");

            RuleFor(c => c.Email)
                .Must(Email.IsValid)
                .WithMessage("O e-mail informado não é válido.");
        }
    }
}
