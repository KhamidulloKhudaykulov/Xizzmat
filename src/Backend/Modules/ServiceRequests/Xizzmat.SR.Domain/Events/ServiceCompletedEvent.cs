using Xizzmat.SR.Domain.Primitives;

namespace Xizzmat.SR.Domain.Events;

public record ServiceCompletedEvent(Guid ServiceId, Guid WorkerId, Guid CustomerId) : DomainEvent;
