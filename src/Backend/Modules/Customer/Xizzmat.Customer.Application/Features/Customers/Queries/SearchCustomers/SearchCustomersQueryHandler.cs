using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Application.Features.Customers.Common;
using Xizzmat.Customer.Domain.Repositories;
using Xizzmat.Customer.Domain.Shared;
using Xizzmat.Customer.Domain.Specifications;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.SearchCustomers;

public class SearchCustomersQueryHandler : IQueryHandler<SearchCustomersQuery, IEnumerable<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly FluentValidation.IValidator<SearchCustomersQuery> _validator;

    public SearchCustomersQueryHandler(ICustomerRepository customerRepository, FluentValidation.IValidator<SearchCustomersQuery> validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<CustomerDto>>> Handle(SearchCustomersQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Failure<IEnumerable<CustomerDto>>(new Error(ResultStatus.ValidationError, validationResult.Errors.First().ErrorMessage));

        var spec = CustomerSpecification.SearchPaged(request.Term, request.Page, request.PageSize);
        var customersResult = await _customerRepository.SelectAllAsync(spec);

        if (!customersResult.IsSuccess)
            return Result.Failure<IEnumerable<CustomerDto>>(customersResult.Error);

        var dtos = customersResult.Value.Select(c => new CustomerDto(c.Id, c.Name, c.Surname, c.PhoneNumber, c.IsDeleted, c.CreatedAt));
        return Result.Success(dtos);
    }
}
