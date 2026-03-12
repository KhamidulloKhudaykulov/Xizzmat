using System.Linq.Expressions;
using Xizzmat.Worker.Domain.Entities;
using Xizzmat.Worker.Domain.Shared;
using Xizzmat.Worker.Domain.Specifications;

namespace Xizzmat.Worker.Domain.Repositories;

public interface IWorkerSkillRepository
{
    Task<Result<WorkerSkill>> InsertAsync(WorkerSkill entity);
    Task<Result<WorkerSkill>> UpdateAsync(WorkerSkill entity);
    Task<Result> DeleteAsync(WorkerSkill entity);
    Task<Result<WorkerSkill>> SelectAsync(Expression<Func<WorkerSkill, bool>> expression, bool asNoTracking = true);
    Task<Result<IEnumerable<WorkerSkill>>> SelectAllAsync(ISpecification<WorkerSkill>? specification = null);
    Task<Result<int>> CountAsync(ISpecification<WorkerSkill>? specification = null);
}
