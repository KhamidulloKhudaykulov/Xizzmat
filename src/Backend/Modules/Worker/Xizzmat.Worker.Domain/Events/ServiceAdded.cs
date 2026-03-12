using Xizzmat.Worker.Domain.Primitives;

namespace Xizzmat.Worker.Domain.Events;

public record ServiceAdded(Guid WorkerId, Guid ServiceId, string Name, decimal Price) : DomainEvent;
