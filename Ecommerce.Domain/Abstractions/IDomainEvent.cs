﻿namespace Ecommerce.Domain.Abstractions
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
