using System.Collections.ObjectModel;

namespace Ecommerce.Domain.Abstractions
{
    public abstract class BaseEntity
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => new ReadOnlyCollection<IDomainEvent>(_domainEvents);

        public Guid Id { get; protected set; } = Guid.NewGuid();

        protected void Raise(IDomainEvent @event) => _domainEvents.Add(@event);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
