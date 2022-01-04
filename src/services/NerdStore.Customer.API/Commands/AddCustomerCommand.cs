using NerdStore.Core.Messages;
using NerdStore.Customer.API.Commands.Validators;
using System;

namespace NerdStore.Customer.API.Commands
{
    public class AddCustomerCommand : Command
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public AddCustomerCommand() {}

        public AddCustomerCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            return new AddCustomerValidator().Validate(this).IsValid;
        }
    }
}
