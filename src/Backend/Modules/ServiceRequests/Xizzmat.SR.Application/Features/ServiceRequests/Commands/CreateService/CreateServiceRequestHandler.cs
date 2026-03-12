using FluentValidation;
using Xizzmat.SR.Application.Abstractions.Messaging;
using Xizzmat.SR.Application.Interfaces;
using Xizzmat.SR.Domain.Entities;
using Xizzmat.SR.Domain.Repositories;
using Xizzmat.SR.Domain.Shared;

namespace Xizzmat.SR.Application.Features.ServiceRequests.Commands.CreateService;

public sealed class CreateServiceRequestHandler : ICommandHandler<CreateServiceRequestCommand, Guid>
{
    private readonly IServiceRequestRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IEventBus _eventBus;

    private readonly IValidator<CreateServiceRequestCommand> _validator;

    public CreateServiceRequestHandler(IServiceRequestRepository repository, IEventBus eventBus, IValidator<CreateServiceRequestCommand> validator, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _eventBus = eventBus;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(CreateServiceRequestCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return Result.Failure<Guid>(new Error(
                ResultStatus.ValidationError, 
                validationResult.Errors.First().ErrorMessage));

        var service = ServiceRequest.Create(request.CustomerId, request.WorkerId).Value;

        await _repository.InsertAsync(service);

        foreach (var @event in service.DomainEvents)
            await _eventBus.Publish(@event);

        service.ClearDomainEvents();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(service.Id);
    }
}
