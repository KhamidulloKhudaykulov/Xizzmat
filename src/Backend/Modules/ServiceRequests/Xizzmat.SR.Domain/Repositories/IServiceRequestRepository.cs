using Xizzmat.SR.Domain.Entities;
using Xizzmat.SR.Domain.Shared;

namespace Xizzmat.SR.Domain.Repositories;

public interface IServiceRequestRepository
{
    Task<Result<ServiceRequest>> InsertAsync(ServiceRequest entity);
    Task<Result<ServiceRequest>> UpdateAsync(ServiceRequest entity);
    Task<Result> DeleteAsync(ServiceRequest entity);
    Task<Result<ServiceRequest>> SelectByIdAsync(Guid id);
}
