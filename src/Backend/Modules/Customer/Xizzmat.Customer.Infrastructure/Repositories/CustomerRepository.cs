using Xizzmat.Customer.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Xizzmat.Customer.Domain.Entities;
using Xizzmat.Customer.Domain.Repositories;
using Xizzmat.Customer.Infrastructure.Database;
using Xizzmat.Customer.Domain.Specifications;

namespace Xizzmat.Customer.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerDbContext _dbContext;
    private readonly DbSet<CustomerEntity> _customers;

    public CustomerRepository(CustomerDbContext dbContext)
    {
        _dbContext = dbContext;
        _customers = _dbContext.Set<CustomerEntity>();
    }

    public Task<Result<CustomerEntity>> InsertAsync(CustomerEntity entity)
    {
        _customers.Add(entity);
        return Task.FromResult(Result.Success(entity));
    }

    public Task<Result<CustomerEntity>> UpdateAsync(CustomerEntity entity)
    {
        _customers.Update(entity);
        return Task.FromResult(Result.Success(entity));
    }

    public Task<Result> DeleteAsync(CustomerEntity entity)
    {
        _customers.Remove(entity);
        return Task.FromResult(Result.Success());
    }

    public async Task<Result<CustomerEntity>> SelectAsync(Expression<Func<CustomerEntity, bool>> expression, bool asNoTracking = true)
    {
        IQueryable<CustomerEntity> query = _customers;

        if (asNoTracking)
            query = query.AsNoTracking();

        var entity = await query.FirstOrDefaultAsync(expression);
        return entity is null
            ? Result.Failure<CustomerEntity>(new Error(
                code: ResultStatus.NotFound,
                message: "Customer not found"))
            : Result.Success(entity);
    }

    public async Task<Result<IEnumerable<CustomerEntity>>> SelectAllAsync(
         ISpecification<CustomerEntity>? specification = null)
    {
        IQueryable<CustomerEntity> query = _customers;

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
        return Result.Success<IEnumerable<CustomerEntity>>(list);
    }

    public async Task<Result<int>> CountAsync(ISpecification<CustomerEntity>? specification = null)
    {
        IQueryable<CustomerEntity> query = _customers;

        if (specification is not null && specification.Criteria is not null)
            query = query.Where(specification.Criteria);

        var count = await query.CountAsync();
        return Result.Success(count);
    }
}