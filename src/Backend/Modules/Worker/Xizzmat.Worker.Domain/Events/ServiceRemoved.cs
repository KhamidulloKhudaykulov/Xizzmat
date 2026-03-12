using Xizzmat.Worker.Domain.Primitives;

namespace Xizzmat.Worker.Domain.Events;

public record ServiceRemoved(Guid WorkerId, Guid ServiceId) : DomainEvent;
