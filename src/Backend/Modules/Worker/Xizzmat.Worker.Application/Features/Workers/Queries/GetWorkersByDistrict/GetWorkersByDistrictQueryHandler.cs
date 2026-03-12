using FluentValidation;
using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Application.Features.Workers.Queries.Common;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;
using Xizzmat.Worker.Domain.Specifications;

namespace Xizzmat.Worker.Application.Features.Workers.Queries.GetWorkersByDistrict;

public class GetWorkersByDistrictQueryHandler : IQueryHandler<GetWorkersByDistrictQuery, List<WorkerDto>>
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IValidator<GetWorkersByDistrictQuery> _validator;

    public GetWorkersByDistrictQueryHandler(IWorkerRepository workerRepository, IValidator<GetWorkersByDistrictQuery> validator)
    {
        _workerRepository = workerRepository;
        _validator = validator;
    }

    public async Task<Result<List<WorkerDto>>> Handle(GetWorkersByDistrictQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Failure<List<WorkerDto>>(new Error(ResultStatus.ValidationError, validationResult.Errors.First().ErrorMessage));

        var workers = await _workerRepository.SelectAllAsync(WorkerSpecifications.ByDistrict(request.City.ToUpper(), request.District.ToUpper()));
        if (workers.IsFailure || !workers.Value.Any())
            return Result.Failure<List<WorkerDto>>(new Error(ResultStatus.NotFound, "Worker not found"));

        var result = workers.Value.Select(w => new WorkerDto(
            w.Id,
            w.Name,
            w.Surname,
            w.Phone,
            w.Email,
            w.City,
            w.Rating,
            w.IsActive,

            w.Services.Select(s => new WorkerServiceDto(
                s.Id,
                s.Name,
                s.Price
            )),

            w.Locations.Select(l => new WorkerLocationDto(
                l.Id,
                l.City,
                l.District
            )),

            w.Skills.Select(s => new WorkerSkillDto(
                s.Id,
                s.Name
            )),

            w.Reviews.Select(r => new WorkerReviewDto(
                r.Id,
                r.Rating,
                r.Comment,
                r.CreatedAt
            ))
        )).ToList();

        return Result.Success(result);
    }
}
