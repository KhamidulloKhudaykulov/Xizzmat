using FluentValidation;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.CreateWorker;

public class CreateWorkerValidator : AbstractValidator<CreateWorkerCommand>
{
    public CreateWorkerValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Phone).NotEmpty();
    }
}
