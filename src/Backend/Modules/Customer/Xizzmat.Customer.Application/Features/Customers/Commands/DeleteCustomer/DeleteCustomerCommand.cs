using Xizzmat.Customer.Application.Abstractions.Messaging;

namespace Xizzmat.Customer.Application.Features.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(
    Guid Id
) : ICommand;
