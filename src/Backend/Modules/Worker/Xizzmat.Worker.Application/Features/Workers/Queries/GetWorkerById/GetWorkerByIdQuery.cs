using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Application.Features.Workers.Queries.Common;

namespace Xizzmat.Worker.Application.Features.Workers.Queries.GetWorkerById;

public record GetWorkerByIdQuery(Guid WorkerId) : IQuery<WorkerDto>;
