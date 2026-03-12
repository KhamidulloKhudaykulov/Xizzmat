using FluentValidation;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetRecentlyRegistered;

public class GetRecentlyRegisteredCustomersQueryValidator : AbstractValidator<GetRecentlyRegisteredCustomersQuery>
{
    public GetRecentlyRegisteredCustomersQueryValidator()
    {
        RuleFor(x => x.Days).GreaterThan(0).WithMessage("Days must be greater than 0");
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be >= 1");
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("PageSize must be >= 1");
    }
}
