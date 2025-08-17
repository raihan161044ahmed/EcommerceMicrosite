using MediatR;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Products.Commands.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _repo;
        public UpdateProductCommandHandler(IProductRepository repo) => _repo = repo;

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken ct)
        {
            var dto = new ProductDto(request.Id, request.Name, request.Description, request.Price, request.StockQuantity, DateTime.UtcNow);
            await _repo.UpdateAsync(dto, ct);
            return Unit.Value;
        }
    }
}
