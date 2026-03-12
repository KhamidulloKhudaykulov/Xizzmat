using FluentValidation;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomerByPhone;

public class GetCustomerByPhoneQueryValidator : AbstractValidator<GetCustomerByPhoneQuery>
{
    public GetCustomerByPhoneQueryValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.");
    }
}
