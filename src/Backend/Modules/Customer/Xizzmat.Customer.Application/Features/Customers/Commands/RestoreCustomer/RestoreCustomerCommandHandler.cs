using FluentValidation;
using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Domain.Repositories;
using Xizzmat.Customer.Domain.Shared;

namespace Xizzmat.Customer.Application.Features.Customers.Commands.RestoreCustomer;

public class RestoreCustomerCommandHandler : ICommandHandler<RestoreCustomerCommand>
{
    private readonly IValidator<RestoreCustomerCommand> _validator;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RestoreCustomerCommandHandler(
        IValidator<RestoreCustomerCommand> validator,
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RestoreCustomerCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Failure(new Error(
                code: ResultStatus.ValidationError,
                message: validationResult.Errors.First().ErrorMessage));

        var existingCustomer = await _customerRepository.SelectAsync(c => c.Id == request.Id);
        if (existingCustomer is null)
            return Result.Failure(new Error(
                code: ResultStatus.NotFound,
                message: "The customer with specified values was not found"));

        existingCustomer.Value.Restore();
        await _customerRepository.UpdateAsync(existingCustomer.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
