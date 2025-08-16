using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.Events
{
    public sealed class OrderCancelledEvent : IDomainEvent
    {
        public Guid OrderId { get; }
        public string Reason { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public OrderCancelledEvent(Guid orderId, string reason)
        {
            OrderId = orderId;
            Reason = reason;
        }
    }
}
