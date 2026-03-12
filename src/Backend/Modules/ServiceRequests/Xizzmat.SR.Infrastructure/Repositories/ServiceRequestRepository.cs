using Microsoft.EntityFrameworkCore;
using Xizzmat.SR.Domain.Repositories;
using Xizzmat.SR.Domain.Shared;
using Xizzmat.SR.Domain.Entities;
using Xizzmat.SR.Infrastructure.Database;

namespace Xizzmat.SR.Infrastructure.Repositories;

public class ServiceRequestRepository : IServiceRequestRepository
{
    private readonly ServiceRequestsDbContext _dbContext;
    private readonly DbSet<ServiceRequest> _requests;

    public ServiceRequestRepository(ServiceRequestsDbContext dbContext)
    {
        _dbContext = dbContext;
        _requests = _dbContext.Set<ServiceRequest>();
    }

    public async Task<Result<ServiceRequest>> InsertAsync(ServiceRequest entity)
    {
        await _requests.AddAsync(entity);
        return Result.Success(entity);
    }

    public Task<Result<ServiceRequest>> UpdateAsync(ServiceRequest entity)
    {
        _requests.Update(entity);
        return Task.FromResult(Result.Success(entity));
    }

    public Task<Result> DeleteAsync(ServiceRequest entity)
    {
        _requests.Remove(entity);
        return Task.FromResult(Result.Success());
    }

    public async Task<Result<ServiceRequest>> SelectByIdAsync(Guid id)
    {
        var req = await _requests.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return req is null
            ? Result.Failure<ServiceRequest>(new Error(ResultStatus.NotFound, "Service request not found"))
            : Result.Success(req);
    }
}
