using Xizzmat.SR.Domain.Primitives;

namespace Xizzmat.SR.Domain.Events;

public record ServiceCreatedEvent(Guid ServiceId, Guid CustomerId) : DomainEvent;
