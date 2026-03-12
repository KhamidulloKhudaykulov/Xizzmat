using Xizzmat.Customer.Application.Abstractions.Messaging;

namespace Xizzmat.Customer.Application.Features.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(
    string Name,
    string Surname,
    string PhoneNumber
) : ICommand<Guid>;
