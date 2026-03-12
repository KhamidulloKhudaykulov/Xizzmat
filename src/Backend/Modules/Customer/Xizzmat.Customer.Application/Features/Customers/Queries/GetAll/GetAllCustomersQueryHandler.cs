using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Application.Features.Customers.Common;
using Xizzmat.Customer.Domain.Repositories;
using Xizzmat.Customer.Domain.Shared;
using Xizzmat.Customer.Domain.Specifications;

namespace Xizzmat.Customer.Application.Features.Customers.Queries;

public class GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly FluentValidation.IValidator<GetAllCustomersQuery> _validator;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, FluentValidation.IValidator<GetAllCustomersQuery> validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<CustomerDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Failure<IEnumerable<CustomerDto>>(new Xizzmat.Customer.Domain.Shared.Error(
                code: Xizzmat.Customer.Domain.Shared.ResultStatus.ValidationError,
                message: validationResult.Errors.First().ErrorMessage));

        // Normalize paging parameters
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 20 : request.PageSize;
        const int maxPageSize = 100;
        if (pageSize > maxPageSize) pageSize = maxPageSize;

        // Use repository-side filtering, ordering, paging to avoid loading large datasets into memory
        var spec = CustomerSpecification.Paged(page, pageSize);
        var customersResult = await _customerRepository.SelectAllAsync(spec);

        if (!customersResult.IsSuccess)
            return Result.Failure<IEnumerable<CustomerDto>>(customersResult.Error);

        var dtos = customersResult.Value.Select(c => new CustomerDto(c.Id, c.Name, c.Surname, c.PhoneNumber, c.IsDeleted, c.CreatedAt));
        return Result.Success<IEnumerable<CustomerDto>>(dtos);
    }
}
