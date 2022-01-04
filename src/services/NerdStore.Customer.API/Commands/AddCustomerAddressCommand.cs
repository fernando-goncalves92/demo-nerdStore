using NerdStore.Core.Messages;
using NerdStore.Customer.API.Commands.Validators;
using System;

namespace NerdStore.Customer.API.Commands
{
    public class AddCustomerAddressCommand : Command
    {
        public Guid CustomerId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public AddCustomerAddressCommand() {}

        public AddCustomerAddressCommand(
            Guid clienteId, 
            string street, 
            string number, 
            string complement,
            string district, 
            string zipCode, 
            string city, 
            string state)
        {
            AggregateId = clienteId;
            CustomerId = clienteId;
            Street = street;
            Number = number;
            Complement = complement;
            District = district;
            ZipCode = zipCode;
            City = city;
            State = state;
        }

        public override bool IsValid()
        {
            return new AddCustomerAddressValidator().Validate(this).IsValid;
        }
    }
}
