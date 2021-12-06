using NerdStore.Core.Messages;
using System;

namespace NerdStore.Order.API.Application.Events
{
    public class OrderPlacedEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public OrderPlacedEvent(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}
