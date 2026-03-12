using Microsoft.EntityFrameworkCore;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;
using Xizzmat.Worker.Domain.Entities;
using Xizzmat.Worker.Domain.Specifications;

namespace Xizzmat.Worker.Infrastructure.Repositories;

public class WorkerServiceRepository : IWorkerServiceRepository
{
    private readonly Xizzmat.Worker.Infrastructure.Database.WorkerDbContext _dbContext;
    private readonly DbSet<WorkerService> _services;

    public WorkerServiceRepository(Xizzmat.Worker.Infrastructure.Database.WorkerDbContext dbContext)
    {
        _dbContext = dbContext;
        _services = _dbContext.Set<WorkerService>();
    }

    public Task<Result<WorkerService>> InsertAsync(WorkerService entity)
    {
        _services.Add(entity);
        return Task.FromResult(Result.Success(entity));
    }

    public Task<Result<WorkerService>> UpdateAsync(WorkerService entity)
    {
        _services.Update(entity);
        return Task.FromResult(Result.Success(entity));
    }

    public Task<Result> DeleteAsync(WorkerService entity)
    {
        _services.Remove(entity);
        return Task.FromResult(Result.Success());
    }

    public async Task<Result<WorkerService>> SelectAsync(System.Linq.Expressions.Expression<Func<WorkerService, bool>> expression, bool asNoTracking = true)
    {
        IQueryable<WorkerService> query = _services;

        if (asNoTracking)
            query = query.AsNoTracking();

        var entity = await query.FirstOrDefaultAsync(expression);
        return entity is null
            ? Result.Failure<WorkerService>(new Error(ResultStatus.NotFound, "Service not found"))
            : Result.Success(entity);
    }

    public async Task<Result<IEnumerable<WorkerService>>> SelectAllAsync(ISpecification<WorkerService>? specification = null)
    {
        IQueryable<WorkerService> query = _services;

        if (specification is not null)
        {
            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            if (specification.AsNoTracking)
                query = query.AsNoTracking();

            if (specification.OrderBy is not null)
                query = specification.OrderBy(query);

            if (specification.Skip.HasValue)
                query = query.Skip(specification.Skip.Value);

            if (specification.Take.HasValue)
                query = query.Take(specification.Take.Value);
        }

        var list = await query.ToListAsync();
        return Result.Success<IEnumerable<WorkerService>>(list);
    }

    public async Task<Result<int>> CountAsync(ISpecification<WorkerService>? specification = null)
    {
        IQueryable<WorkerService> query = _services;

        if (specification is not null && specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        var count = await query.CountAsync();
        return Result.Success(count);
    }
}
