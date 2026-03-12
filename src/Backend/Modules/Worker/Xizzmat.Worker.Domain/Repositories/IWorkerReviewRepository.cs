using System.Linq.Expressions;
using Xizzmat.Worker.Domain.Entities;
using Xizzmat.Worker.Domain.Shared;
using Xizzmat.Worker.Domain.Specifications;

namespace Xizzmat.Worker.Domain.Repositories;

public interface IWorkerReviewRepository
{
    Task<Result<WorkerReview>> InsertAsync(WorkerReview entity);
    Task<Result<WorkerReview>> UpdateAsync(WorkerReview entity);
    Task<Result> DeleteAsync(WorkerReview entity);
    Task<Result<WorkerReview>> SelectAsync(Expression<Func<WorkerReview, bool>> expression, bool asNoTracking = true);
    Task<Result<IEnumerable<WorkerReview>>> SelectAllAsync(ISpecification<WorkerReview>? specification = null);
    Task<Result<int>> CountAsync(ISpecification<WorkerReview>? specification = null);
}
