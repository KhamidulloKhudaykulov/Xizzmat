using System.Linq.Expressions;
using Xizzmat.Worker.Domain.Entities;
using Xizzmat.Worker.Domain.Shared;
using Xizzmat.Worker.Domain.Specifications;

namespace Xizzmat.Worker.Domain.Repositories;

public interface IWorkerLocationRepository
{
    Task<Result<WorkerLocation>> InsertAsync(WorkerLocation entity);
    Task<Result<WorkerLocation>> UpdateAsync(WorkerLocation entity);
    Task<Result> DeleteAsync(WorkerLocation entity);
    Task<Result<WorkerLocation>> SelectAsync(Expression<Func<WorkerLocation, bool>> expression, bool asNoTracking = true);
    Task<Result<IEnumerable<WorkerLocation>>> SelectAllAsync(ISpecification<WorkerLocation>? specification = null);
    Task<Result<int>> CountAsync(ISpecification<WorkerLocation>? specification = null);
}
