namespace Xizzmat.Worker.Application.Features.Workers.Queries.Common;

public record WorkerReviewDto(
    Guid Id,
    int Rating,
    string? Comment,
    DateTime CreatedAt
);
