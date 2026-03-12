using Microsoft.EntityFrameworkCore;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;
using Xizzmat.Worker.Domain.Entities;
using Xizzmat.Worker.Domain.Specifications;
using Xizzmat.Worker.Infrastructure.Database;
using System.Linq.Expressions;

namespace Xizzmat.Worker.Infrastructure.Repositories;

public class WorkerRepository : IWorkerRepository
{
    private readonly WorkerDbContext _dbContext;
    private readonly DbSet<WorkerEntity> _workers;

    public WorkerRepository(WorkerDbContext dbContext)
    {
        _dbContext = dbContext;
        _workers = _dbContext.Set<WorkerEntity>();
    }

    public Task<Result<WorkerEntity>> InsertAsync(WorkerEntity entity)
    {
        _workers.Add(entity);
        return Task.FromResult(Result.Success(entity));
    }

    public Task<Result<WorkerEntity>> UpdateAsync(WorkerEntity entity)
    {
        _workers.Update(entity);
        return Task.FromResult(Result.Success(entity));
    }

    public Task<Result> DeleteAsync(WorkerEntity entity)
    {
        _workers.Remove(entity);
        return Task.FromResult(Result.Success());
    }

    public async Task<Result<WorkerEntity>> SelectAsync(Expression<Func<WorkerEntity, bool>> expression, bool asNoTracking = true)
    {
        IQueryable<WorkerEntity> query = _workers;

        if (asNoTracking)
            query = query.AsNoTracking();

        var entity = await query.FirstOrDefaultAsync(expression);
        return entity is null
            ? Result.Failure<WorkerEntity>(new Error(ResultStatus.NotFound, "Worker not found"))
            : Result.Success(entity);
    }

    public async Task<Result<IEnumerable<WorkerEntity>>> SelectAllAsync(
        ISpecification<WorkerEntity>? specification = null)
    {
        IQueryable<WorkerEntity> query = _workers;

        if (specification is not null)
        {
            if (specification.AsNoTracking)
                query = query.AsNoTracking();

            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            foreach (var include in specification.Includes)
                query = query.Include(include);

            if (specification.OrderBy is not null)
                query = specification.OrderBy(query);

            if (specification.Skip.HasValue)
                query = query.Skip(specification.Skip.Value);

            if (specification.Take.HasValue)
                query = query.Take(specification.Take.Value);
        }

        var list = await query.ToListAsync();

        return Result.Success<IEnumerable<WorkerEntity>>(list);
    }

    public async Task<Result<int>> CountAsync(ISpecification<WorkerEntity>? specification = null)
    {
        IQueryable<WorkerEntity> query = _workers;

        if (specification is not null && specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        var count = await query.CountAsync();
        return Result.Success(count);
    }
}
