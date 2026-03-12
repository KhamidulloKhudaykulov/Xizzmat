using FluentValidation;
using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.AddLocation;

public class AddLocationCommandHandler : ICommandHandler<AddLocationCommand>
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IWorkerLocationRepository _workerLocationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddLocationCommand> _validator;

    public AddLocationCommandHandler(
        IWorkerLocationRepository workerLocationRepository,
        IUnitOfWork unitOfWork,
        IValidator<AddLocationCommand> validator,
        IWorkerRepository workerRepository)
    {
        _workerLocationRepository = workerLocationRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _workerRepository = workerRepository;
    }

    public async Task<Result> Handle(AddLocationCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return Result.Failure(new Error(
                ResultStatus.ValidationError, 
                validationResult.Errors.First().ErrorMessage));

        var existingWorker = await _workerRepository.SelectAsync(w => w.Id == request.WorkerId);
        if (existingWorker is null)
            return Result.Failure(new Error(ResultStatus.NotFound, "Worker not found"));

        existingWorker.Value.AddLocation(request.City.ToUpper(), request.District.ToUpper());
        await _workerLocationRepository.InsertAsync(existingWorker.Value.Locations.Last());
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}