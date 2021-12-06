using System;
using NerdStore.Core.Messages;

namespace NerdStore.Core.SharedEvents
{
    public class OrderPlacedIntegrationEvent : IntegrationEvent
    {
        public Guid CustomerId { get; private set; }

        public OrderPlacedIntegrationEvent(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
