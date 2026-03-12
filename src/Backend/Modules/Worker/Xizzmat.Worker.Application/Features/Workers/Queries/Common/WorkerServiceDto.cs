namespace Xizzmat.Worker.Application.Features.Workers.Queries.Common;

public record WorkerServiceDto(
    Guid Id,
    string Name,
    decimal Price
);
