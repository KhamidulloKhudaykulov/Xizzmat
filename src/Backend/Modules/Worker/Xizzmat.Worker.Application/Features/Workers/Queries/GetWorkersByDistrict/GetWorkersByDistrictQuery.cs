using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Application.Features.Workers.Queries.Common;

namespace Xizzmat.Worker.Application.Features.Workers.Queries.GetWorkersByDistrict;

public record GetWorkersByDistrictQuery(
    string City,
    string District,
    int Page = 1,
    int PageSize = 10) : IQuery<List<WorkerDto>>;
