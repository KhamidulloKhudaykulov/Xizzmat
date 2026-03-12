using Xizzmat.Worker.Domain.Primitives;

namespace Xizzmat.Worker.Domain.Events;

public record WorkerCreated(Guid WorkerId) : DomainEvent;
