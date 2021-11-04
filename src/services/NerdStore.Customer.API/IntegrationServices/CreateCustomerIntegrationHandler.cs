using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NerdStore.Core.Mediator;
using NerdStore.Core.Messages;
using NerdStore.Core.SharedEvents;
using NerdStore.Customer.API.Commands;
using NerdStore.MessageBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Customer.API.IntegrationServices
{
    public class CreateCustomerIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CreateCustomerIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetRespond();

            _bus.AdvancedBus.Connected += (s, e) => 
            {
                SetRespond();
            };

            return Task.CompletedTask;
        }

        private void SetRespond()
        {
            _bus.RespondAsync<AddedUserIntegrationEvent, ResponseMessage>(async request => await CreateCustomer(request));
        }

        private async Task<ResponseMessage> CreateCustomer(AddedUserIntegrationEvent message)
        {
            var customerCommand = new AddCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);

            ValidationResult success;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                success = await mediator.SendCommand(customerCommand);
            }

            return new ResponseMessage(success);
        }
    }
}
