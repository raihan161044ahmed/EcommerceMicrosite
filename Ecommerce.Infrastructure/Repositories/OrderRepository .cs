using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Mappings;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db) => _db = db;

        public async Task<IEnumerable<OrderDto>> GetAllAsync(CancellationToken ct)
            => await _db.Orders
                .Include("_items")
                .AsNoTracking()
                .Select(o => o.ToDto())
                .ToListAsync(ct);

        public async Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.Orders
                .Include("_items")
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id, ct);

            return entity?.ToDto();
        }

        public async Task<Guid> CreateAsync(OrderDto dto, CancellationToken ct)
        {
            var entity = dto.ToDomainNew();
            _db.Orders.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task CancelAsync(Guid id, CancellationToken ct)
        {
            var entity = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id, ct)
                ?? throw new InvalidOperationException("Order not found.");

            entity.Cancel("Cancelled by admin");
            await _db.SaveChangesAsync(ct);
        }
    }
}
