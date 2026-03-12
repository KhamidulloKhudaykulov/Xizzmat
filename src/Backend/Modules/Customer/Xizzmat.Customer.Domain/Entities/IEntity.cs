using Xizzmat.Customer.Domain.Shared;

namespace Xizzmat.Customer.Domain.Entities;

public interface IEntity
{
    Guid Id { get; set; }
    DateTime CreatedAt { get; set; }
    bool IsDeleted { get; set; }

    Result Delete();
    Result Restore();
}
