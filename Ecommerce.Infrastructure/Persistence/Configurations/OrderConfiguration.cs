using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> b)
        {
            b.ToTable("Orders");
            b.HasKey(x => x.Id);

            b.Property(x => x.CustomerId).IsRequired();
            b.Property(x => x.Status).IsRequired();

            b.OwnsOne(x => x.TotalAmount, money =>
            {
                money.Property(m => m.Amount).HasColumnName("TotalAmount").HasColumnType("decimal(18,2)");
                money.Property(m => m.Currency).HasColumnName("TotalCurrency").HasMaxLength(3);
            });

            b.OwnsOne(x => x.ShippingAddress, addr =>
            {
                addr.Property(a => a.Street).HasColumnName("ShippingAddress_Street").HasMaxLength(200).IsRequired();
                addr.Property(a => a.City).HasColumnName("ShippingAddress_City").HasMaxLength(100).IsRequired();
                addr.Property(a => a.State).HasColumnName("ShippingAddress_State").HasMaxLength(100).IsRequired();
                addr.Property(a => a.ZipCode).HasColumnName("ShippingAddress_ZipCode").HasMaxLength(20).IsRequired();
            });

            b.HasMany(typeof(OrderItem), "_items").WithOne().HasForeignKey("OrderId").OnDelete(DeleteBehavior.Cascade);

            b.Property(x => x.CreatedAt).IsRequired();
            b.Property(x => x.UpdatedAt);
        }
    }
}
