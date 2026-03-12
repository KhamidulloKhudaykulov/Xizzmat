using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Application.Features.Customers.Common;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomers;

public record GetCustomersQuery(int Page = 1, int PageSize = 20) : IQuery<IEnumerable<CustomerDto>>;
