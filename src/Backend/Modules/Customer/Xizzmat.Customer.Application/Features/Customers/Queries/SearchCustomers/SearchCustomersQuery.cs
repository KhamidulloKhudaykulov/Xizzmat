using Xizzmat.Customer.Application.Abstractions.Messaging;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.SearchCustomers;

public record SearchCustomersQuery(string Term, int Page = 1, int PageSize = 20) : IQuery<IEnumerable<CustomerDto>>;
