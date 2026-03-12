using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Application.Features.Customers.Common;
using Xizzmat.Customer.Domain.Repositories;
using Xizzmat.Customer.Domain.Shared;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomerByPhone;

public class GetCustomerByPhoneQueryHandler : IQueryHandler<GetCustomerByPhoneQuery, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly FluentValidation.IValidator<GetCustomerByPhoneQuery> _validator;

    public GetCustomerByPhoneQueryHandler(ICustomerRepository customerRepository, FluentValidation.IValidator<GetCustomerByPhoneQuery> validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task<Result<CustomerDto>> Handle(GetCustomerByPhoneQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Failure<CustomerDto>(new Error(ResultStatus.ValidationError, validationResult.Errors.First().ErrorMessage));

        var spec = Xizzmat.Customer.Domain.Specifications.CustomerSpecification.ByPhone(request.PhoneNumber);
        var customersResult = await _customerRepository.SelectAllAsync(spec);
        if (!customersResult.IsSuccess || !customersResult.Value.Any())
            return Result.Failure<CustomerDto>(new Error(ResultStatus.NotFound, "Customer not found"));

        var entity = customersResult.Value.First();
        var dto = new CustomerDto(entity.Id, entity.Name, entity.Surname, entity.PhoneNumber, entity.IsDeleted, entity.CreatedAt);
        return Result.Success(dto);
    }
}
