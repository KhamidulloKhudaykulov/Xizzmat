using Xizzmat.Worker.Domain.Primitives;

namespace Xizzmat.Worker.Domain.Events;

public record LocationAdded(Guid WorkerId, Guid LocationId, string City, string District) : DomainEvent;
