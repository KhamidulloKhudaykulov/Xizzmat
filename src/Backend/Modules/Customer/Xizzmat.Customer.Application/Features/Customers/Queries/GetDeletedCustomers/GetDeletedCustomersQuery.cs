using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Application.Features.Customers.Common;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetDeletedCustomers;

public record GetDeletedCustomersQuery(int Page = 1, int PageSize = 20) : IQuery<IEnumerable<CustomerDto>>;
