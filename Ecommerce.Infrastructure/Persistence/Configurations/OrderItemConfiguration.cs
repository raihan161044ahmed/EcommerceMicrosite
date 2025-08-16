using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> b)
        {
            b.ToTable("OrderItems");
            b.HasKey(x => x.Id);

            b.Property<Guid>("OrderId");

            b.Property(x => x.ProductId).IsRequired();
            b.Property(x => x.Quantity).IsRequired();

            b.OwnsOne(x => x.UnitPrice, price =>
            {
                price.Property(p => p.Amount).HasColumnName("UnitPrice").HasColumnType("decimal(18,2)").IsRequired();
                price.Property(p => p.Currency).HasColumnName("UnitCurrency").HasMaxLength(3).IsRequired();
            });
        }
    }
}
