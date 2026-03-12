using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Domain.Repositories;
using Xizzmat.Customer.Domain.Shared;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomersCount;

public class GetCustomersCountQueryHandler : IQueryHandler<GetCustomersCountQuery, int>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly FluentValidation.IValidator<GetCustomersCountQuery> _validator;

    public GetCustomersCountQueryHandler(ICustomerRepository customerRepository, FluentValidation.IValidator<GetCustomersCountQuery> validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(GetCustomersCountQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Failure<int>(new Error(ResultStatus.ValidationError, validationResult.Errors.First().ErrorMessage));

        var countResult = await _customerRepository.CountAsync(null);
        if (!countResult.IsSuccess)
            return Result.Failure<int>(countResult.Error);

        return Result.Success(countResult.Value);
    }
}
