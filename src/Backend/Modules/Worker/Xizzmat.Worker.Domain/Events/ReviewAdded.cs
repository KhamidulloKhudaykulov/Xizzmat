using Xizzmat.Worker.Domain.Primitives;

namespace Xizzmat.Worker.Domain.Events;

public record ReviewAdded(Guid WorkerId, Guid ReviewId, int Rating, string? Comment) : DomainEvent;
