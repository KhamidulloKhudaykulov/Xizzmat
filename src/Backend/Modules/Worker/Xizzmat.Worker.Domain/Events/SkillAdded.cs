using Xizzmat.Worker.Domain.Primitives;

namespace Xizzmat.Worker.Domain.Events;

public record SkillAdded(Guid WorkerId, Guid SkillId, string Name) : DomainEvent;
