using System;
using System.Linq.Expressions;

namespace Xizzmat.Worker.Domain.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; }

    List<Expression<Func<T, object>>> Includes { get; }

    int? Skip { get; }
    int? Take { get; }
    bool AsNoTracking { get; }
}
