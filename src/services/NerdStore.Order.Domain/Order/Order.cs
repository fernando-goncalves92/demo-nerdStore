using NerdStore.Core.DomainObjects;

namespace NerdStore.Order.Domain.Order
{
    public class Order : Entity, IAggregateRoot
    {
        protected Order() { }
    }
}
