using Xizzmat.Customer.Application.Abstractions.Messaging;

namespace Xizzmat.Customer.Application.Features.Customers.Queries;

public record GetCustomerByIdQuery(Guid Id) : IQuery<CustomerDto>;
