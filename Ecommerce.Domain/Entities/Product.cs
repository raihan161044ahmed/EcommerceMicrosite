using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Events;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Domain.Entities
{
    public sealed class Product : BaseEntity
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Money Price { get; private set; }
        public int StockQuantity { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private const int LowStockThreshold = 5;

        private Product() { } // EF

        public Product(string name, Money price, int stockQuantity, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Product name is required.");
            if (stockQuantity < 0) throw new DomainException("Stock cannot be negative.");

            Name = name.Trim();
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string name, string? description, Money price)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Product name is required.");
            Name = name.Trim();
            Description = description;
            Price = price;
            UpdatedAt = DateTime.UtcNow;
        }

        public void IncreaseStock(int amount)
        {
            if (amount <= 0) throw new DomainException("Increase amount must be positive.");
            StockQuantity += amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DecreaseStock(int amount)
        {
            if (amount <= 0) throw new DomainException("Decrease amount must be positive.");
            if (amount > StockQuantity) throw new DomainException("Insufficient stock.");
            StockQuantity -= amount;
            UpdatedAt = DateTime.UtcNow;

            if (StockQuantity <= LowStockThreshold)
                Raise(new ProductStockLowEvent(Id, StockQuantity));
        }
    }
}
