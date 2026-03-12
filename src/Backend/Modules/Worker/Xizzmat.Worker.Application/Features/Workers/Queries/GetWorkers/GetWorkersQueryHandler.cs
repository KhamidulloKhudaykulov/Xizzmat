using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Application.Features.Workers.Queries.Common;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;
using Xizzmat.Worker.Domain.Specifications;

namespace Xizzmat.Worker.Application.Features.Workers.Queries.GetWorkers;

public class GetWorkersQueryHandler : IQueryHandler<GetWorkersQuery, IEnumerable<WorkerDto>>
{
    private readonly IWorkerRepository _workerRepository;

    public GetWorkersQueryHandler(IWorkerRepository workerRepository)
    {
        _workerRepository = workerRepository;
    }

    public async Task<Result<IEnumerable<WorkerDto>>> Handle(GetWorkersQuery request, CancellationToken cancellationToken)
    {
        var filter = WorkerSpecifications.Filter(request.City, request.District, request.Skill, request.MinRating, request.MaxRating, request.PageNumber, request.PageSize);
        var workers = await _workerRepository.SelectAllAsync(filter);

        if (workers.IsFailure || !workers.Value.Any())
            return Result.Failure<IEnumerable<WorkerDto>>(new Error(ResultStatus.NotFound, "Worker not found"));

        var dtos = workers.Value.Select(w => new WorkerDto(
            w.Id,
            w.Name,
            w.Surname,
            w.Phone,
            w.Email,
            w.City,
            w.Rating,
            w.IsActive,
            w.Services.Select(s => new WorkerServiceDto(s.Id, s.Name, s.Price)),
            w.Locations.Select(l => new WorkerLocationDto(l.Id, l.City, l.District)),
            w.Skills.Select(s => new WorkerSkillDto(s.Id, s.Name)),
            w.Reviews.Select(r => new WorkerReviewDto(r.Id, r.Rating, r.Comment, r.CreatedAt))
        ));

        return Result.Success(dtos);
    }
}
