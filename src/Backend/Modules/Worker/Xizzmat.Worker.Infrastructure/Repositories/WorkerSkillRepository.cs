using Microsoft.EntityFrameworkCore;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;
using Xizzmat.Worker.Domain.Entities;
using Xizzmat.Worker.Domain.Specifications;

namespace Xizzmat.Worker.Infrastructure.Repositories;

public class WorkerSkillRepository : IWorkerSkillRepository
{
    private readonly Xizzmat.Worker.Infrastructure.Database.WorkerDbContext _dbContext;
    private readonly DbSet<WorkerSkill> _skills;

    public WorkerSkillRepository(Xizzmat.Worker.Infrastructure.Database.WorkerDbContext dbContext)
    {
        _dbContext = dbContext;
        _skills = _dbContext.Set<WorkerSkill>();
    }

    public Task<Result<WorkerSkill>> InsertAsync(WorkerSkill entity)
    {
        _skills.Add(entity);
        return Task.FromResult(Result.Success(entity));
    }

    public Task<Result<WorkerSkill>> UpdateAsync(WorkerSkill entity)
    {
        _skills.Update(entity);
        return Task.FromResult(Result.Success(entity));
    }

    public Task<Result> DeleteAsync(WorkerSkill entity)
    {
        _skills.Remove(entity);
        return Task.FromResult(Result.Success());
    }

    public async Task<Result<WorkerSkill>> SelectAsync(System.Linq.Expressions.Expression<Func<WorkerSkill, bool>> expression, bool asNoTracking = true)
    {
        IQueryable<WorkerSkill> query = _skills;

        if (asNoTracking)
            query = query.AsNoTracking();

        var entity = await query.FirstOrDefaultAsync(expression);
        return entity is null
            ? Result.Failure<WorkerSkill>(new Error(ResultStatus.NotFound, "Skill not found"))
            : Result.Success(entity);
    }

    public async Task<Result<IEnumerable<WorkerSkill>>> SelectAllAsync(ISpecification<WorkerSkill>? specification = null)
    {
        IQueryable<WorkerSkill> query = _skills;

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
        return Result.Success<IEnumerable<WorkerSkill>>(list);
    }

    public async Task<Result<int>> CountAsync(ISpecification<WorkerSkill>? specification = null)
    {
        IQueryable<WorkerSkill> query = _skills;

        if (specification is not null && specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        var count = await query.CountAsync();
        return Result.Success(count);
    }
}
