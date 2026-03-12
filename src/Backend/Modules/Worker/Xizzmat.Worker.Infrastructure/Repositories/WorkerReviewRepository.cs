using Microsoft.EntityFrameworkCore;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;
using Xizzmat.Worker.Domain.Entities;
using Xizzmat.Worker.Domain.Specifications;

namespace Xizzmat.Worker.Infrastructure.Repositories;

public class WorkerReviewRepository : IWorkerReviewRepository
{
    private readonly Xizzmat.Worker.Infrastructure.Database.WorkerDbContext _dbContext;
    private readonly DbSet<WorkerReview> _reviews;

    public WorkerReviewRepository(Xizzmat.Worker.Infrastructure.Database.WorkerDbContext dbContext)
    {
        _dbContext = dbContext;
        _reviews = _dbContext.Set<WorkerReview>();
    }

    public Task<Result<WorkerReview>> InsertAsync(WorkerReview entity)
    {
        _reviews.Add(entity);
        return Task.FromResult(Result.Success(entity));
    }

    public Task<Result<WorkerReview>> UpdateAsync(WorkerReview entity)
    {
        _reviews.Update(entity);
        return Task.FromResult(Result.Success(entity));
    }

    public Task<Result> DeleteAsync(WorkerReview entity)
    {
        _reviews.Remove(entity);
        return Task.FromResult(Result.Success());
    }

    public async Task<Result<WorkerReview>> SelectAsync(System.Linq.Expressions.Expression<Func<WorkerReview, bool>> expression, bool asNoTracking = true)
    {
        IQueryable<WorkerReview> query = _reviews;

        if (asNoTracking)
            query = query.AsNoTracking();

        var entity = await query.FirstOrDefaultAsync(expression);
        return entity is null
            ? Result.Failure<WorkerReview>(new Error(ResultStatus.NotFound, "Review not found"))
            : Result.Success(entity);
    }

    public async Task<Result<IEnumerable<WorkerReview>>> SelectAllAsync(ISpecification<WorkerReview>? specification = null)
    {
        IQueryable<WorkerReview> query = _reviews;

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
        return Result.Success<IEnumerable<WorkerReview>>(list);
    }

    public async Task<Result<int>> CountAsync(ISpecification<WorkerReview>? specification = null)
    {
        IQueryable<WorkerReview> query = _reviews;

        if (specification is not null && specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        var count = await query.CountAsync();
        return Result.Success(count);
    }
}
