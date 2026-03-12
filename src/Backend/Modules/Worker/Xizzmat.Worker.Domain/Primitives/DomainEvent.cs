namespace Xizzmat.Worker.Domain.Primitives;

public abstract record DomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
