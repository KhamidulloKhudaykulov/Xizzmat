using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Application.Features.Customers.Common;
using Xizzmat.Customer.Domain.Repositories;
using Xizzmat.Customer.Domain.Shared;
using Xizzmat.Customer.Domain.Specifications;

namespace Xizzmat.Customer.Application.Features.Customers.Queries;

public class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly FluentValidation.IValidator<GetCustomerByIdQuery> _validator;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, FluentValidation.IValidator<GetCustomerByIdQuery> validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task<Result<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Failure<CustomerDto>(new Error(
                code: ResultStatus.ValidationError,
                message: validationResult.Errors.First().ErrorMessage));

        var customersResult = await _customerRepository.SelectAllAsync(CustomerSpecification.ById(request.Id));
        if (!customersResult.IsSuccess || !customersResult.Value.Any())
            return Result.Failure<CustomerDto>(new Error(
                code: ResultStatus.NotFound,
                message: "The customer with specified values was not found"));

        var entity = customersResult.Value.First();
        var dto = new CustomerDto(entity.Id, entity.Name, entity.Surname, entity.PhoneNumber, entity.IsDeleted, entity.CreatedAt);
        return Result.Success(dto);
    }
}
