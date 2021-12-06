using MediatR;
using NerdStore.Core.SharedEvents;
using NerdStore.MessageBus;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Order.API.Application.Events
{
    public class OrderEventHandler : INotificationHandler<OrderPlacedEvent>
    {
        private readonly IMessageBus _bus;

        public OrderEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(OrderPlacedEvent message, CancellationToken cancellationToken)
        {
            await _bus.PublishAsync(new OrderPlacedIntegrationEvent(message.CustomerId));
        }
    }
}
