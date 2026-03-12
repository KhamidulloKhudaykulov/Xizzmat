using Xizzmat.Customer.Application.Abstractions.Messaging;

namespace Xizzmat.Customer.Application.Features.Customers.Commands.UpdateProfile;

public record UpdateProfileCommand(
    Guid Id,
    string Name,
    string Surname,
    string PhoneNumber) : ICommand;
