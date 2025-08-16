using System.Reflection;
using Ecommerce.Domain.Abstractions;
using Ecommerce.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator)
            : base(options) => _mediator = mediator;

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            // Gather domain events before saving
            var domainEntities = ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity)
                .ToArray();

            var events = domainEntities
                .SelectMany(e => e.DomainEvents)
                .ToArray();

            var result = await base.SaveChangesAsync(ct);

            // Dispatch & clear
            foreach (var domainEvent in events)
                await _mediator.Publish(domainEvent, ct);

            foreach (var entity in domainEntities)
                entity.ClearDomainEvents();

            return result;
        }
    }
}
