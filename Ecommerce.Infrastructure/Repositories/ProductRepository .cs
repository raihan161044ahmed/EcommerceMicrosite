using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Mappings;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) => _db = db;

        public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken ct)
            => await _db.Products.AsNoTracking().Select(p => p.ToDto()).ToListAsync(ct);

        public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
            return entity?.ToDto();
        }

        public async Task<Guid> CreateAsync(ProductDto dto, CancellationToken ct)
        {
            var entity = dto.ToDomainNew();
            _db.Products.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task UpdateAsync(ProductDto dto, CancellationToken ct)
        {
            var entity = await _db.Products.FirstOrDefaultAsync(x => x.Id == dto.Id, ct)
                ?? throw new InvalidOperationException("Product not found.");

            dto.ApplyToDomain(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}
