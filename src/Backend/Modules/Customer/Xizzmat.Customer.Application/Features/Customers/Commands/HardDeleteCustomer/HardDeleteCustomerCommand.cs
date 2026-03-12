using Xizzmat.Customer.Application.Abstractions.Messaging;

namespace Xizzmat.Customer.Application.Features.Customers.Commands.HardDeleteCustomer;

public record HardDeleteCustomerCommand(
    Guid Id
) : ICommand;
