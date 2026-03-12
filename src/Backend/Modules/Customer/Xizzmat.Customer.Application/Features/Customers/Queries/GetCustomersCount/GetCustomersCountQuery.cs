using Xizzmat.Customer.Application.Abstractions.Messaging;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomersCount;

public record GetCustomersCountQuery() : IQuery<int>;
