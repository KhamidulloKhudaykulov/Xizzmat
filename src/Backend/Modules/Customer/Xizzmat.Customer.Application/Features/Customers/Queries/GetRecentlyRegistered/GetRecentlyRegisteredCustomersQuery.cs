using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Application.Features.Customers.Common;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetRecentlyRegistered;

public record GetRecentlyRegisteredCustomersQuery(int Days = 1, int Page = 1, int PageSize = 20) : IQuery<IEnumerable<CustomerDto>>;
