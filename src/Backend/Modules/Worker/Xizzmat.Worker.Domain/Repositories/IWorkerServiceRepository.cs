using System.Linq.Expressions;
using Xizzmat.Worker.Domain.Entities;
using Xizzmat.Worker.Domain.Shared;
using Xizzmat.Worker.Domain.Specifications;

namespace Xizzmat.Worker.Domain.Repositories;

public interface IWorkerServiceRepository
{
    Task<Result<WorkerService>> InsertAsync(WorkerService entity);
    Task<Result<WorkerService>> UpdateAsync(WorkerService entity);
    Task<Result> DeleteAsync(WorkerService entity);
    Task<Result<WorkerService>> SelectAsync(Expression<Func<WorkerService, bool>> expression, bool asNoTracking = true);
    Task<Result<IEnumerable<WorkerService>>> SelectAllAsync(ISpecification<WorkerService>? specification = null);
    Task<Result<int>> CountAsync(ISpecification<WorkerService>? specification = null);
}
