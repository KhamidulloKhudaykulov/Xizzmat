using System;
using System.Collections.Generic;
using Xizzmat.Worker.Domain.Shared;

namespace Xizzmat.Worker.Domain.Primitives;

/// <summary>
/// Base aggregate root for domain entities.
/// Provides basic IEntity implementation and domain event handling.
/// </summary>
public abstract class AggregateRoot : IEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsDeleted { get; set; } = false;

    // Domain events support
    private List<DomainEvent>? _domainEvents;
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents is null ? Array.Empty<DomainEvent>() : _domainEvents.AsReadOnly();

    protected void AddDomainEvent(DomainEvent @event)
    {
        _domainEvents ??= new List<DomainEvent>();
        _domainEvents.Add(@event);
    }

    protected void RemoveDomainEvent(DomainEvent @event)
    {
        _domainEvents?.Remove(@event);
    }

    protected void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public virtual Result Delete()
    {
        IsDeleted = true;
        return Result.Success();
    }

    public virtual Result Restore()
    {
        IsDeleted = false;
        return Result.Success();
    }
}
