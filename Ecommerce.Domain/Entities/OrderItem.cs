using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Domain.Entities
{
    public sealed class OrderItem : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public Money UnitPrice { get; private set; }

        private OrderItem() { }

        public OrderItem(Guid productId, int quantity, Money unitPrice)
        {
            if (productId == Guid.Empty) throw new DomainException("ProductId is required.");
            if (quantity <= 0) throw new DomainException("Quantity must be greater than zero.");

            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public Money Subtotal() => UnitPrice.Multiply(Quantity);
    }
}
