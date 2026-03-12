using FluentValidation;

namespace Xizzmat.Worker.Application.Features.Workers.Queries.GetWorkerById;

public class GetWorkerByIdValidator : AbstractValidator<GetWorkerByIdQuery>
{
    public GetWorkerByIdValidator()
    {
        RuleFor(x => x.WorkerId)
            .NotEmpty().WithMessage("Worker ID is required.");
    }
}
