using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.DeleteWorker;

public class DeleteWorkerCommandHandler : ICommandHandler<DeleteWorkerCommand>
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteWorkerCommandHandler(IWorkerRepository workerRepository, IUnitOfWork unitOfWork)
    {
        _workerRepository = workerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteWorkerCommand request, CancellationToken cancellationToken)
    {
        var existing = await _workerRepository.SelectAsync(w => w.Id == request.Id);
        if (!existing.IsSuccess)
            return Result.Failure(existing.Error);

        existing.Value.Delete();
        var delResult = await _workerRepository.UpdateAsync(existing.Value);
        if (!delResult.IsSuccess)
            return Result.Failure(delResult.Error);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
