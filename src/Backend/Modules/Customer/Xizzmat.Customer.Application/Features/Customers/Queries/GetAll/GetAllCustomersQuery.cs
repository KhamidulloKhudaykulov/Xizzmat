using Xizzmat.Customer.Application.Abstractions.Messaging;

namespace Xizzmat.Customer.Application.Features.Customers.Queries;

public record GetAllCustomersQuery(int Page = 1, int PageSize = 20) : IQuery<IEnumerable<CustomerDto>>;
