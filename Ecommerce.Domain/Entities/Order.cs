using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Events;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Domain.Entities
{
    public sealed class Order : BaseEntity
    {
        private readonly List<OrderItem> _items = new();

        public Guid CustomerId { get; private set; }
        public Address ShippingAddress { get; private set; } = null!;
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public Money TotalAmount { get; private set; } = Money.Zero();
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private Order() { } // EF

        private Order(Guid customerId, Address shippingAddress)
        {
            if (customerId == Guid.Empty) throw new DomainException("CustomerId is required.");
            CustomerId = customerId;
            ShippingAddress = shippingAddress;
            CreatedAt = DateTime.UtcNow;
        }

        public static Order Create(Guid customerId, Address shippingAddress, IEnumerable<OrderItem> items)
        {
            if (items is null || !items.Any()) throw new DomainException("Order must have items.");

            var order = new Order(customerId, shippingAddress);
            order._items.AddRange(items);
            order.RecalculateTotal();
            order.Raise(new OrderCreatedEvent(order.Id, order.CustomerId));
            return order;
        }

        public void AddItem(OrderItem item)
        {
            if (Status != OrderStatus.Pending) throw new DomainException("Cannot modify a non-pending order.");
            _items.Add(item);
            RecalculateTotal();
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel(string reason = "Cancelled by user")
        {
            if (Status == OrderStatus.Cancelled) return;
            if (Status == OrderStatus.Completed) throw new DomainException("Cannot cancel a completed order.");
            Status = OrderStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
            Raise(new OrderCancelledEvent(Id, reason));
        }

        public void MarkPaid()
        {
            if (Status != OrderStatus.Pending) throw new DomainException("Only pending orders can be paid.");
            Status = OrderStatus.Paid;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkShipped()
        {
            if (Status != OrderStatus.Paid) throw new DomainException("Only paid orders can be shipped.");
            Status = OrderStatus.Shipped;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Complete()
        {
            if (Status != OrderStatus.Shipped) throw new DomainException("Only shipped orders can be completed.");
            Status = OrderStatus.Completed;
            UpdatedAt = DateTime.UtcNow;
        }

        private void RecalculateTotal()
        {
            var currency = _items.FirstOrDefault()?.UnitPrice.Currency ?? "USD";
            TotalAmount = _items.Aggregate(Money.Zero(currency), (acc, i) => acc.Add(i.Subtotal()));
        }
    }
}
