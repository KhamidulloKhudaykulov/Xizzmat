namespace Xizzmat.Worker.Application.Features.Workers.Queries.Common;

public record WorkerDto(
    Guid id,
    string name,
    string surname,
    string phone,
    string? email,
    string city,
    double rating,
    bool isActive,
    IEnumerable<WorkerServiceDto> services,
    IEnumerable<WorkerLocationDto> locations,
    IEnumerable<WorkerSkillDto> skills,
    IEnumerable<WorkerReviewDto> reviews);
