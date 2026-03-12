using FluentValidation;
using Xizzmat.SR.Application.Abstractions.Messaging;
using Xizzmat.SR.Application.Interfaces;
using Xizzmat.SR.Domain.Repositories;
using Xizzmat.SR.Domain.Shared;

namespace Xizzmat.SR.Application.Features.ServiceRequests.Commands.CancelService;

public sealed class CancelServiceRequestHandler
    : ICommandHandler<CancelServiceRequestCommand>
{
    private readonly IServiceRequestRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IEventBus _eventBus;
    private readonly IValidator<CancelServiceRequestCommand> _validator;

    public CancelServiceRequestHandler(
        IServiceRequestRepository repository,
        IEventBus eventBus,
        IValidator<CancelServiceRequestCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _eventBus = eventBus;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CancelServiceRequestCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Failure(new Error(
                ResultStatus.ValidationError,
                validationResult.Errors.First().ErrorMessage));

        var serviceResult = await _repository.SelectByIdAsync(request.ServiceId);
        if (serviceResult == null)
            return Result.Failure(new Error(ResultStatus.NotFound, "Service not found"));

        var service = serviceResult.Value;

        var result = service.Cancel();
        if (result.IsFailure)
            return result;

        await _repository.UpdateAsync(service);

        foreach (var @event in service.DomainEvents)
            await _eventBus.Publish(@event);

        service.ClearDomainEvents();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
