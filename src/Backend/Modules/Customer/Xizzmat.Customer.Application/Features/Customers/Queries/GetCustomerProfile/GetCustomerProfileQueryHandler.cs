using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Application.Features.Customers.Common;
using Xizzmat.Customer.Domain.Repositories;
using Xizzmat.Customer.Domain.Shared;
using Xizzmat.Customer.Domain.Specifications;

namespace Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomerProfile;

public class GetCustomerProfileQueryHandler : IQueryHandler<GetCustomerProfileQuery, CustomerProfileDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly FluentValidation.IValidator<GetCustomerProfileQuery> _validator;

    public GetCustomerProfileQueryHandler(ICustomerRepository customerRepository, FluentValidation.IValidator<GetCustomerProfileQuery> validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task<Result<CustomerProfileDto>> Handle(GetCustomerProfileQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Failure<CustomerProfileDto>(new Error(ResultStatus.ValidationError, validationResult.Errors.First().ErrorMessage));

        var spec = CustomerSpecification.ById(request.Id);
        var customersResult = await _customerRepository.SelectAllAsync(spec);
        if (customersResult.IsFailure || !customersResult.Value.Any())
            return Result.Failure<CustomerProfileDto>(new Error(ResultStatus.NotFound, "Customer not found"));

        var entity = customersResult.Value.First();
        var dto = new CustomerProfileDto(entity.Id, entity.Name, entity.Surname, entity.PhoneNumber);
        return Result.Success(dto);
    }
}
