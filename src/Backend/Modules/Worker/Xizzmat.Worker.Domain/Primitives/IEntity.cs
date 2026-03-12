using Xizzmat.Worker.Domain.Shared;

namespace Xizzmat.Worker.Domain.Primitives;

public interface IEntity
{
    Guid Id { get; protected set; }
    DateTime CreatedAt { get; protected set; }
    bool IsDeleted { get; protected set; }

    Result Delete();
    Result Restore();
}
