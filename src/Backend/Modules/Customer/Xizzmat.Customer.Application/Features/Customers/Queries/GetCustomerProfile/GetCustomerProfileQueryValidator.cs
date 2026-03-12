using FluentValidation;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomerProfile;

public class GetCustomerProfileQueryValidator : AbstractValidator<GetCustomerProfileQuery>
{
    public GetCustomerProfileQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Customer ID is required.")
            .Must(id => id != Guid.Empty).WithMessage("Customer ID must be a valid GUID.");
    }
}
