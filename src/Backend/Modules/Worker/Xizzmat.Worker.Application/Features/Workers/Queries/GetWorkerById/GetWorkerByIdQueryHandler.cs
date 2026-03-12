using FluentValidation;
using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Application.Features.Workers.Queries.Common;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;
using Xizzmat.Worker.Domain.Specifications;

namespace Xizzmat.Worker.Application.Features.Workers.Queries.GetWorkerById;

public class GetWorkerByIdQueryHandler : IQueryHandler<GetWorkerByIdQuery, WorkerDto>
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IValidator<GetWorkerByIdQuery> _validator;

    public GetWorkerByIdQueryHandler(IWorkerRepository workerRepository, IValidator<GetWorkerByIdQuery> validator)
    {
        _workerRepository = workerRepository;
        _validator = validator;
    }

    public async Task<Result<WorkerDto>> Handle(GetWorkerByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return Result.Failure<WorkerDto>(new Error(ResultStatus.ValidationError, validationResult.Errors.First().ErrorMessage));

        var existingWorker = await _workerRepository.SelectAllAsync(WorkerSpecifications.ById(request.WorkerId));
        if (existingWorker.IsFailure || !existingWorker.Value.Any())
            return Result.Failure<WorkerDto>(new Error(ResultStatus.NotFound, "Worker not found"));

        var worker = existingWorker.Value.First();
        var workerResult = new WorkerDto(
            id: worker.Id,
            name: worker.Name,
            surname: worker.Surname,
            phone: worker.Phone,
            email: worker.Email ?? string.Empty,
            city: worker.City,
            rating: worker.Rating,
            isActive: worker.IsActive,
            services: worker.Services.Select(s => new WorkerServiceDto(s.Id, s.Name, s.Price)).ToList(),
            locations: worker.Locations.Select(l => new WorkerLocationDto(l.Id, l.City, l.District)).ToList(),
            skills: worker.Skills.Select(s => new WorkerSkillDto(s.Id, s.Name)).ToList(),
            reviews: worker.Reviews.Select(r => new WorkerReviewDto(r.Id, r.Rating, r.Comment ?? string.Empty, r.CreatedAt)).ToList());

        return Result.Success(workerResult);
    }
}
