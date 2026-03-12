using FluentValidation;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.SearchCustomers;

public class SearchCustomersQueryValidator : AbstractValidator<SearchCustomersQuery>
{
    public SearchCustomersQueryValidator()
    {
        RuleFor(x => x.Term).NotEmpty().WithMessage("Search term is required.");
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be >= 1");
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("PageSize must be >= 1");
    }
}
