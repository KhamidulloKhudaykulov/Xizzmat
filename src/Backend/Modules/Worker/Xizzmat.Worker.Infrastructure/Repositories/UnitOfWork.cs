using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Infrastructure.Database;

namespace Xizzmat.Worker.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly WorkerDbContext _dbContext;

    public UnitOfWork(WorkerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
