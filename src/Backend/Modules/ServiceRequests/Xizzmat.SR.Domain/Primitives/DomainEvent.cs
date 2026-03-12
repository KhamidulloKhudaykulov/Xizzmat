namespace Xizzmat.SR.Domain.Primitives;

public abstract record DomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
