using FluentValidation;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomersCount;

public class GetCustomersCountQueryValidator : AbstractValidator<GetCustomersCountQuery>
{
    public GetCustomersCountQueryValidator()
    {
        // no parameters to validate currently
    }
}
