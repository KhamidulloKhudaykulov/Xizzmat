using Xizzmat.Customer.Domain.Shared;
using System.Linq.Expressions;
using Xizzmat.Customer.Domain.Entities;

namespace Xizzmat.Customer.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Result<CustomerEntity>> InsertAsync(CustomerEntity entity);
    Task<Result<CustomerEntity>> UpdateAsync(CustomerEntity entity);
    Task<Result> DeleteAsync(CustomerEntity entity);
    Task<Result<CustomerEntity>> SelectAsync(
        Expression<Func<CustomerEntity, bool>> expression,
        bool asNoTracking = true);

    // Support specification-based queries for better encapsulation
    Task<Result<IEnumerable<CustomerEntity>>> SelectAllAsync(
        Xizzmat.Customer.Domain.Specifications.ISpecification<CustomerEntity>? specification = null);

    Task<Result<int>> CountAsync(Xizzmat.Customer.Domain.Specifications.ISpecification<CustomerEntity>? specification = null);
}
