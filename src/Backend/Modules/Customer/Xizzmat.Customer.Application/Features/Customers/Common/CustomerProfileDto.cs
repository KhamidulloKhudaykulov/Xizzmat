namespace Xizzmat.Customer.Application.Features.Customers.Common;

public record CustomerProfileDto(
    Guid Id,
    string Name,
    string Surname,
    string PhoneNumber
);
