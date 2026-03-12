using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Application.Features.Customers.Common;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomerByPhone;

public record GetCustomerByPhoneQuery(string PhoneNumber) : IQuery<CustomerDto>;
