using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Application.Features.Customers.Common;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomerProfile;

public record GetCustomerProfileQuery(Guid Id) : IQuery<CustomerProfileDto>;
