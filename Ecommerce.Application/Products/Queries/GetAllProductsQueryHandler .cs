using MediatR;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Products.Queries
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;
}

namespace Ecommerce.Application.Products.Queries.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _repo;
        public GetAllProductsQueryHandler(IProductRepository repo) => _repo = repo;

        public Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken ct)
            => _repo.GetAllAsync(ct);
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        private readonly IProductRepository _repo;
        public GetProductByIdQueryHandler(IProductRepository repo) => _repo = repo;

        public Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken ct)
            => _repo.GetByIdAsync(request.Id, ct);
    }
}
