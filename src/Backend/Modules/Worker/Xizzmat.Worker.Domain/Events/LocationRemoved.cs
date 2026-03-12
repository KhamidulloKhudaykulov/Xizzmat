using Xizzmat.Worker.Domain.Primitives;

namespace Xizzmat.Worker.Domain.Events;

public record LocationRemoved(Guid WorkerId, Guid LocationId) : DomainEvent;
