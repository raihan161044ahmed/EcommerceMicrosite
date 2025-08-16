using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.Events
{
    public sealed class ProductStockLowEvent : IDomainEvent
    {
        public Guid ProductId { get; }
        public int StockQuantity { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public ProductStockLowEvent(Guid productId, int stockQuantity)
        {
            ProductId = productId;
            StockQuantity = stockQuantity;
        }
    }
}
