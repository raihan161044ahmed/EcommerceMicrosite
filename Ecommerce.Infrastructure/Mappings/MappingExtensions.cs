using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.ValueObjects;
using System.Net;

namespace Ecommerce.Infrastructure.Mappings
{
    internal static class MappingExtensions
    {
        // Product
        public static ProductDto ToDto(this Product p) =>
            new(p.Id, p.Name, p.Description, p.Price.Amount, p.StockQuantity, p.CreatedAt);

        public static Product ToDomainNew(this ProductDto dto) =>
            new(dto.Name, new Money(dto.Price), dto.StockQuantity, dto.Description);

        public static void ApplyToDomain(this ProductDto dto, Product dest)
        {
            dest.UpdateDetails(dto.Name, dto.Description, new Money(dto.Price));
            if (dto.StockQuantity > dest.StockQuantity)
                dest.IncreaseStock(dto.StockQuantity - dest.StockQuantity);
            else if (dto.StockQuantity < dest.StockQuantity)
                dest.DecreaseStock(dest.StockQuantity - dto.StockQuantity);
        }

        // Order
        public static OrderDto ToDto(this Order o) =>
            new(o.Id, o.CustomerId, o.TotalAmount.Amount, o.Status.ToString(),
                o.Items.Select(i => new OrderItemDto(i.ProductId, i.Quantity, i.UnitPrice.Amount)).ToList(),
                o.CreatedAt);

        public static Order ToDomainNew(this OrderDto dto)
        {
            var address = new Address("N/A", "N/A", "N/A", "N/A"); // you set real one in Application if needed
            var items = dto.Items.Select(i => new OrderItem(i.ProductId, i.Quantity, new Money(i.UnitPrice))).ToList();
            return Order.Create(dto.CustomerId, address, items);
        }

        public static void Cancel(this Order o) => o.Cancel("Cancelled by repository call");
        public static OrderStatus ParseStatus(string s) => Enum.TryParse<OrderStatus>(s, true, out var st) ? st : OrderStatus.Pending;
    }
}
