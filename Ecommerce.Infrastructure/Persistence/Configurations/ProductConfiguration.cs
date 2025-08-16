using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> b)
        {
            b.ToTable("Products");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.Description).HasMaxLength(4000);

            b.OwnsOne(x => x.Price, price =>
            {
                price.Property(p => p.Amount).HasColumnName("Price").HasColumnType("decimal(18,2)").IsRequired();
                price.Property(p => p.Currency).HasColumnName("Currency").HasMaxLength(3).IsRequired();
            });

            b.Property(x => x.StockQuantity).IsRequired();
            b.Property(x => x.CreatedAt).IsRequired();
            b.Property(x => x.UpdatedAt);
        }
    }
}
