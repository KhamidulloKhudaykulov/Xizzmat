// This file is deprecated; DTO moved to Common. Kept for compatibility.
namespace Xizzmat.Customer.Application.Features.Customers.Queries;

public record CustomerDto(Guid Id, string Name, string Surname, string PhoneNumber, bool IsDeleted, DateTime CreatedAt);
