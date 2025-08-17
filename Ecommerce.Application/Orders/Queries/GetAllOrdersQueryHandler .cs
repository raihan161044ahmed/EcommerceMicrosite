using MediatR;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Orders.Queries
{
    public record GetAllOrdersQuery() : IRequest<IEnumerable<OrderDto>>;
    public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto?>;
}

namespace Ecommerce.Application.Orders.Queries.Handlers
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _repo;
        public GetAllOrdersQueryHandler(IOrderRepository repo) => _repo = repo;

        public Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken ct)
            => _repo.GetAllAsync(ct);
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
    {
        private readonly IOrderRepository _repo;
        public GetOrderByIdQueryHandler(IOrderRepository repo) => _repo = repo;

        public Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken ct)
            => _repo.GetByIdAsync(request.Id, ct);
    }
}
