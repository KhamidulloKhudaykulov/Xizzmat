using Microsoft.EntityFrameworkCore;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;
using Xizzmat.Worker.Domain.Entities;
using Xizzmat.Worker.Domain.Specifications;

namespace Xizzmat.Worker.Infrastructure.Repositories;

public class WorkerLocationRepository : IWorkerLocationRepository
{
    private readonly Xizzmat.Worker.Infrastructure.Database.WorkerDbContext _dbContext;
    private readonly DbSet<WorkerLocation> _locations;

    public WorkerLocationRepository(Xizzmat.Worker.Infrastructure.Database.WorkerDbContext dbContext)
    {
        _dbContext = dbContext;
        _locations = _dbContext.Set<WorkerLocation>();
    }

    public Task<Result<WorkerLocation>> InsertAsync(WorkerLocation entity)
    {
        _locations.Add(entity);
        return Task.FromResult(Result.Success(entity));
    }

    public Task<Result<WorkerLocation>> UpdateAsync(WorkerLocation entity)
    {
        _locations.Update(entity);
        return Task.FromResult(Result.Success(entity));
    }

    public Task<Result> DeleteAsync(WorkerLocation entity)
    {
        _locations.Remove(entity);
        return Task.FromResult(Result.Success());
    }

    public async Task<Result<WorkerLocation>> SelectAsync(System.Linq.Expressions.Expression<Func<WorkerLocation, bool>> expression, bool asNoTracking = true)
    {
        IQueryable<WorkerLocation> query = _locations;

        if (asNoTracking)
            query = query.AsNoTracking();

        var entity = await query.FirstOrDefaultAsync(expression);
        return entity is null
            ? Result.Failure<WorkerLocation>(new Error(ResultStatus.NotFound, "Location not found"))
            : Result.Success(entity);
    }

    public async Task<Result<IEnumerable<WorkerLocation>>> SelectAllAsync(ISpecification<WorkerLocation>? specification = null)
    {
        IQueryable<WorkerLocation> query = _locations;

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
        return Result.Success<IEnumerable<WorkerLocation>>(list);
    }

    public async Task<Result<int>> CountAsync(ISpecification<WorkerLocation>? specification = null)
    {
        IQueryable<WorkerLocation> query = _locations;

        if (specification is not null && specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        var count = await query.CountAsync();
        return Result.Success(count);
    }
}
