using MediatR;
using NerdStore.Customer.API.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Customer.API.Events
{
    public class CustomerEventHandler : INotificationHandler<AddedCustomerEvent>
    {
        public Task Handle(AddedCustomerEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
