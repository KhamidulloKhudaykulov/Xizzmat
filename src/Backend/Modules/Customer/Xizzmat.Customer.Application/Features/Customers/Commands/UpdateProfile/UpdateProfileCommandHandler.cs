using FluentValidation;
using Xizzmat.Customer.Application.Abstractions.Messaging;
using Xizzmat.Customer.Domain.Repositories;
using Xizzmat.Customer.Domain.Shared;

namespace Xizzmat.Customer.Application.Features.Customers.Commands.UpdateProfile;

public class UpdateProfileCommandHandler 
    : ICommandHandler<UpdateProfileCommand>
{
    private readonly IValidator<UpdateProfileCommand> _validator;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProfileCommandHandler(
        IUnitOfWork unitOfWork, 
        ICustomerRepository customerRepository, 
        IValidator<UpdateProfileCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result.Failure<Guid>(new Error(
                code: ResultStatus.ValidationError,
                message: errors));
        }
        var customer = await _customerRepository.SelectAsync(c => c.Id == request.Id);
        if (customer is null)
        {
            return Result.Failure(new Error(
                code: ResultStatus.NotFound,
                message: "The customer with specified values was not found"));
        }

        customer.Value.UpdateProfile(request.Name, request.Surname, request.PhoneNumber);
        await _customerRepository.UpdateAsync(customer.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
