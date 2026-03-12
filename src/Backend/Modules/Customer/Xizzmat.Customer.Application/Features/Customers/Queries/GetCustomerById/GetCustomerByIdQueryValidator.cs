using FluentValidation;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Customer ID is required.")
            .Must(id => id != Guid.Empty).WithMessage("Customer ID must be a valid GUID.");
    }
}
