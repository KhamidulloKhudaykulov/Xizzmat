using FluentValidation;
using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.AddService;

public class AddServiceCommandHandler : ICommandHandler<AddServiceCommand>
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IWorkerServiceRepository _workerServiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IValidator<AddServiceCommand> _validator;

    public AddServiceCommandHandler(
        IValidator<AddServiceCommand> validator, 
        IWorkerRepository workerRepository, 
        IWorkerServiceRepository workerServiceRepository, 
        IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _workerRepository = workerRepository;
        _workerServiceRepository = workerServiceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddServiceCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return Result.Failure(new Error(ResultStatus.ValidationError, validationResult.Errors.First().ErrorMessage));

        var existingWorker = await _workerRepository.SelectAsync(w => w.Id == request.WorkerId);
        if (existingWorker is null)
            return Result.Failure(new Error(ResultStatus.NotFound, "Worker not found"));

        existingWorker.Value.AddService(request.Name, request.Price);
        await _workerServiceRepository.InsertAsync(existingWorker.Value.Services.Last());
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}