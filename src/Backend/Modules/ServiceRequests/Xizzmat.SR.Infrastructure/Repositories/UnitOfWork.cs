using Xizzmat.SR.Domain.Repositories;
using Xizzmat.SR.Infrastructure.Database;

namespace Xizzmat.SR.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ServiceRequestsDbContext _dbContext;

    public UnitOfWork(ServiceRequestsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return _dbContext.SaveChangesAsync(ct);
    }
}
