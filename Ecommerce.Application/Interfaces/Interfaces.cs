namespace Ecommerce.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken ct);
        Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<Guid> CreateAsync(ProductDto dto, CancellationToken ct);
        Task UpdateAsync(ProductDto dto, CancellationToken ct);
    }

    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDto>> GetAllAsync(CancellationToken ct);
        Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<Guid> CreateAsync(OrderDto dto, CancellationToken ct);
        Task CancelAsync(Guid id, CancellationToken ct);
    }

    public interface IJwtTokenService
    {
        string GenerateToken(string username, string role);
    }

    // DTOs to keep API separated from Domain entities
    public record ProductDto(Guid Id, string Name, string? Description, decimal Price, int StockQuantity, DateTime CreatedAt);
    public record OrderItemDto(Guid ProductId, int Quantity, decimal UnitPrice);
    public record OrderDto(Guid Id, Guid CustomerId, decimal TotalAmount, string Status, List<OrderItemDto> Items, DateTime CreatedAt);
}
