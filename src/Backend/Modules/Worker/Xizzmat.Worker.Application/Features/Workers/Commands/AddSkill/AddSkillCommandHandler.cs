using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Domain.Entities;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.AddSkill;

public class AddSkillCommandHandler : ICommandHandler<AddSkillCommand>
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IWorkerSkillRepository _workerSkillRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddSkillCommandHandler(
        IWorkerRepository workerRepository, 
        IUnitOfWork unitOfWork, 
        IWorkerSkillRepository workerSkillRepository)
    {
        _workerRepository = workerRepository;
        _unitOfWork = unitOfWork;
        _workerSkillRepository = workerSkillRepository;
    }

    public async Task<Result> Handle(AddSkillCommand request, CancellationToken cancellationToken)
    {
        var existing = await _workerRepository.SelectAsync(w => w.Id == request.WorkerId, asNoTracking: true);
        if (!existing.IsSuccess)
            return Result.Failure(existing.Error);

        var workerSkill = new WorkerSkill(Guid.NewGuid(), existing.Value.Id, request.Name);

        var res = existing.Value.AddSkill(workerSkill.Name);
        if (!res.IsSuccess) return Result.Failure(res.Error);

        await _workerSkillRepository.InsertAsync(workerSkill);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
