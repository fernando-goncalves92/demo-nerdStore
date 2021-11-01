using FluentValidation.Results;
using MediatR;
using NerdStore.Core.Messages;
using NerdStore.Customer.API.Data;
using NerdStore.Customer.API.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Customer.API.Commands
{
    public class CustomerCommandHandler :
        CommandHandler,
        IRequestHandler<AddCustomerCommand, ValidationResult>,
        IRequestHandler<AddCustomerAddressCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var customer = new Entities.Customer(request.Id, request.Name, request.Email, request.Cpf);
            var customerAlreadyExists = await _customerRepository.GetByCpf(customer.Cpf.Number);

            if (customerAlreadyExists != null)
            {
                AddError("Este CPF já está cadastrado no sistema!");

                return ValidationResult;
            }

            _customerRepository.Add(customer);

            customer.AddEvent(new AddedCustomerEvent(request.Id, request.Name, request.Email, request.Cpf));

            return await Commit(_customerRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AddCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var address = new Address(
                request.Street, 
                request.Number, 
                request.Complement, 
                request.District, 
                request.ZipCode, 
                request.City, 
                request.State, 
                request.CustomerId);

            _customerRepository.AddAddress(address);

            return await Commit(_customerRepository.UnitOfWork);
        }
    }
}
