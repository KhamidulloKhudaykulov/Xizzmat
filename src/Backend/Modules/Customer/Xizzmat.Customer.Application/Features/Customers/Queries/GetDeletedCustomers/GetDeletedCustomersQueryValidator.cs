using FluentValidation;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetDeletedCustomers;

public class GetDeletedCustomersQueryValidator : AbstractValidator<GetDeletedCustomersQuery>
{
    public GetDeletedCustomersQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be >= 1");
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("PageSize must be >= 1");

        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(100).WithMessage("PageSize must be <= 100");
    }
}
