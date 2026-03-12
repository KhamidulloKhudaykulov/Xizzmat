using FluentValidation;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomers;

public class GetCustomersQueryValidator : AbstractValidator<GetCustomersQuery>
{
    public GetCustomersQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be >= 1");
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("PageSize must be >= 1");

        RuleFor(x => x.PageSize).LessThanOrEqualTo(100).WithMessage("PageSize must be <= 100");
    }
}
