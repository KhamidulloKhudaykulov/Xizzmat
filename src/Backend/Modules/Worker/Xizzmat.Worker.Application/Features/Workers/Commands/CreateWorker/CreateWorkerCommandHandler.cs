using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Domain.Entities;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.CreateWorker;

public class CreateWorkerCommandHandler : ICommandHandler<CreateWorkerCommand, Guid>
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWorkerCommandHandler(IWorkerRepository workerRepository, IUnitOfWork unitOfWork)
    {
        _workerRepository = workerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
    {
        var workerResult = WorkerEntity.Create(Guid.Empty, request.Name, request.Surname, request.Phone, request.Email, request.City);
        if (!workerResult.IsSuccess)
            return Result.Failure<Guid>(workerResult.Error);

        var insertResult = await _workerRepository.InsertAsync(workerResult.Value);
        if (!insertResult.IsSuccess)
            return Result.Failure<Guid>(insertResult.Error);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(workerResult.Value.Id);
    }
}
