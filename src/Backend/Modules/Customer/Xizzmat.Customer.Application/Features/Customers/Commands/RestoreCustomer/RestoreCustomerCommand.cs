using Xizzmat.Customer.Application.Abstractions.Messaging;

namespace Xizzmat.Customer.Application.Features.Customers.Commands.RestoreCustomer;

public record RestoreCustomerCommand(
    Guid Id
) : ICommand;
