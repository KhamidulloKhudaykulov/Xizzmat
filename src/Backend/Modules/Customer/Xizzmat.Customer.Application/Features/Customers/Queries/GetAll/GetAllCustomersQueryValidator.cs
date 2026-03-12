using FluentValidation;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetAll;

public class GetAllCustomersQueryValidator : AbstractValidator<GetAllCustomersQuery>
{
    public GetAllCustomersQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be >= 1");
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("PageSize must be >= 1");
    }
}
