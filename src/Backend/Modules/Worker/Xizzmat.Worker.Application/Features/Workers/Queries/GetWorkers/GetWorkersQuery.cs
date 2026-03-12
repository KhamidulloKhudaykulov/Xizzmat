using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Application.Features.Workers.Queries.Common;

namespace Xizzmat.Worker.Application.Features.Workers.Queries.GetWorkers;

public record GetWorkersQuery(
    string? City,
    string? District,
    string? Skill,
    double? MinRating,
    double? MaxRating,
    int PageNumber = 1,
    int PageSize = 10) : IQuery<IEnumerable<WorkerDto>>;
