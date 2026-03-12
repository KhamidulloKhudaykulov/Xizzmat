using Xizzmat.SR.Domain.Shared;

namespace Xizzmat.SR.Domain.Primitives;

public interface IEntity
{
    Guid Id { get; protected set; }
    DateTime CreatedAt { get; protected set; }
    bool IsDeleted { get; protected set; }

    Result Delete();
    Result Restore();
}
