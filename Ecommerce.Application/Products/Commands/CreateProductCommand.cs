using MediatR;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Products.Commands
{
    public record CreateProductCommand(string Name, string? Description, decimal Price, int StockQuantity)
        : IRequest<Guid>;

    public record UpdateProductCommand(Guid Id, string Name, string? Description, decimal Price, int StockQuantity)
        : IRequest<Unit>;
}

namespace Ecommerce.Application.Products.Commands.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _repo;
        public CreateProductCommandHandler(IProductRepository repo) => _repo = repo;

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken ct)
        {
            var dto = new ProductDto(
                Guid.NewGuid(),
                request.Name,
                request.Description,
                request.Price,
                request.StockQuantity,
                DateTime.UtcNow
            );
            return await _repo.CreateAsync(dto, ct);
        }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _repo;
        public UpdateProductCommandHandler(IProductRepository repo) => _repo = repo;

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken ct)
        {
            var dto = new ProductDto(
                request.Id,
                request.Name,
                request.Description,
                request.Price,
                request.StockQuantity,
                DateTime.UtcNow
            );

            await _repo.UpdateAsync(dto, ct);
            return Unit.Value;
        }
    }
}
