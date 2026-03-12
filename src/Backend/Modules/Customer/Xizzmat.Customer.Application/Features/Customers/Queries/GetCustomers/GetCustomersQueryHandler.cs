using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Application.Features.Customers.Common;
using Xizzmat.Customer.Domain.Repositories;
using Xizzmat.Customer.Domain.Shared;
using Xizzmat.Customer.Domain.Specifications;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomers;

public class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, IEnumerable<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly FluentValidation.IValidator<GetCustomersQuery> _validator;

    public GetCustomersQueryHandler(ICustomerRepository customerRepository, FluentValidation.IValidator<GetCustomersQuery> validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<CustomerDto>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Failure<IEnumerable<CustomerDto>>(new Error(ResultStatus.ValidationError, validationResult.Errors.First().ErrorMessage));

        var spec = CustomerSpecification.Paged(request.Page, request.PageSize);
        var customersResult = await _customerRepository.SelectAllAsync(spec);

        if (!customersResult.IsSuccess)
            return Result.Failure<IEnumerable<CustomerDto>>(customersResult.Error);

        var dtos = customersResult.Value.Select(c => new CustomerDto(c.Id, c.Name, c.Surname, c.PhoneNumber, c.IsDeleted, c.CreatedAt));
        return Result.Success(dtos);
    }
}
