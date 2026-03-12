using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.UpdateWorker;

public class UpdateWorkerCommandHandler : ICommandHandler<UpdateWorkerCommand>
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateWorkerCommandHandler(IWorkerRepository workerRepository, IUnitOfWork unitOfWork)
    {
        _workerRepository = workerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
    {
        var existing = await _workerRepository.SelectAsync(w => w.Id == request.Id);
        if (!existing.IsSuccess)
            return Result.Failure(existing.Error);

        var worker = existing.Value;
        worker.GetType().GetProperty("Name")!.SetValue(worker, request.Name);
        worker.GetType().GetProperty("Surname")!.SetValue(worker, request.Surname);
        worker.GetType().GetProperty("Phone")!.SetValue(worker, request.Phone);
        worker.GetType().GetProperty("Email")!.SetValue(worker, request.Email);
        worker.GetType().GetProperty("City")!.SetValue(worker, request.City);

        var updateResult = await _workerRepository.UpdateAsync(worker);
        if (!updateResult.IsSuccess)
            return Result.Failure(updateResult.Error);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
