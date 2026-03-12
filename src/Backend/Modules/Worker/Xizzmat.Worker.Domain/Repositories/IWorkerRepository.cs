using System.Linq.Expressions;
using Xizzmat.Worker.Domain.Entities;
using Xizzmat.Worker.Domain.Shared;
using Xizzmat.Worker.Domain.Specifications;

namespace Xizzmat.Worker.Domain.Repositories;

public interface IWorkerRepository
{
    Task<Result<WorkerEntity>> InsertAsync(WorkerEntity entity);
    Task<Result<WorkerEntity>> UpdateAsync(WorkerEntity entity);
    Task<Result> DeleteAsync(WorkerEntity entity);
    Task<Result<WorkerEntity>> SelectAsync(Expression<Func<WorkerEntity, bool>> expression, bool asNoTracking = true);

    Task<Result<IEnumerable<WorkerEntity>>> SelectAllAsync(ISpecification<WorkerEntity>? specification = null);
    Task<Result<int>> CountAsync(ISpecification<WorkerEntity>? specification = null);
}
