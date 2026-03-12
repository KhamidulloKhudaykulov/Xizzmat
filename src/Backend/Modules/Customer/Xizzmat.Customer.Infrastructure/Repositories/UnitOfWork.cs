using Xizzmat.Customer.Domain.Repositories;
using Xizzmat.Customer.Infrastructure.Database;

namespace Xizzmat.Customer.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CustomerDbContext _dbContext;

    public UnitOfWork(CustomerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
