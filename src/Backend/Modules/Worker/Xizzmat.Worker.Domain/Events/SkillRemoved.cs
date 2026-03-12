using Xizzmat.Worker.Domain.Primitives;

namespace Xizzmat.Worker.Domain.Events;

public record SkillRemoved(Guid WorkerId, Guid SkillId) : DomainEvent;
