using FluentValidation;
using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Domain.Repositories;
using Xizzmat.Customer.Domain.Shared;

namespace Xizzmat.Customer.Application.Features.Customers.Commands.HardDeleteCustomer;

public class HardDeleteCustomerCommandHandler : ICommandHandler<HardDeleteCustomerCommand>
{
    private readonly IValidator<HardDeleteCustomerCommand> _validator;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HardDeleteCustomerCommandHandler(
        IValidator<HardDeleteCustomerCommand> validator,
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(HardDeleteCustomerCommand request, CancellationToken cancellationToken)
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

        existingCustomer.Value.Delete();
        await _customerRepository.DeleteAsync(existingCustomer.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
