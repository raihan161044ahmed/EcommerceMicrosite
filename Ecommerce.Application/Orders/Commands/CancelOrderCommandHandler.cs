using MediatR;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Orders.Commands.Handlers
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IOrderRepository _repo;
        public CancelOrderCommandHandler(IOrderRepository repo) => _repo = repo;

        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken ct)
        {
            await _repo.CancelAsync(request.OrderId, ct);
            return Unit.Value;
        }
    }
}
