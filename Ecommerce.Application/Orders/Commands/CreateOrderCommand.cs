using MediatR;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Orders.Commands
{
    public record CreateOrderCommand(Guid CustomerId, string ShippingStreet, string ShippingCity, string ShippingState, string ShippingZipCode, List<OrderItemDto> Items)
        : IRequest<Guid>;

    public record CancelOrderCommand(Guid OrderId) : IRequest;
}

namespace Ecommerce.Application.Orders.Commands.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _repo;
        public CreateOrderCommandHandler(IOrderRepository repo) => _repo = repo;

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken ct)
        {
            var total = request.Items.Sum(i => i.UnitPrice * i.Quantity);
            var dto = new OrderDto(Guid.NewGuid(), request.CustomerId, total, "Pending", request.Items, DateTime.UtcNow);
            return await _repo.CreateAsync(dto, ct);
        }
    }
}
