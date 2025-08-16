using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.Events
{
    public sealed class OrderCreatedEvent : IDomainEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public OrderCreatedEvent(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}
