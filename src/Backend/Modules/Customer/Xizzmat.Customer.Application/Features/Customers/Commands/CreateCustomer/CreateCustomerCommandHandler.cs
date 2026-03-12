using Xizzmat.Customer.Domain.Shared;
using FluentValidation;
using FluentValidation.Results;
using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Domain.Entities;
using Xizzmat.Customer.Domain.Repositories;

namespace Xizzmat.Customer.Application.Features.Customers.Commands.CreateCustomer;

public sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCustomerCommand> _validator;

    public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IValidator<CreateCustomerCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result.Failure<Guid>(new Error(
                code: ResultStatus.ValidationError,
                message: errors));
        }

        var customer = CustomerEntity.Create(Guid.NewGuid(), request.Name, request.Surname, request.PhoneNumber);
        if (customer.IsFailure)
            return await Task.FromResult(Result.Failure<Guid>(customer.Error));

        var existingCustomer = await _customerRepository.SelectAsync(c => c.PhoneNumber == request.PhoneNumber);
        if (existingCustomer.Error.Code != ResultStatus.NotFound)
            return Result.Failure<Guid>(new Error(
                code: ResultStatus.Conflict,
                message: "A customer with the same phone number already exists."));

        var insertResult = await _customerRepository.InsertAsync(customer.Value);

        if (!insertResult.IsSuccess)
            return Result.Failure<Guid>(insertResult.Error);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(customer.Value.Id);
    }
}
