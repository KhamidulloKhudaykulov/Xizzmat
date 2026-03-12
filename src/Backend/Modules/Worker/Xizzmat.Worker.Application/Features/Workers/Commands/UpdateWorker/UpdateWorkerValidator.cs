using FluentValidation;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.UpdateWorker;

public class UpdateWorkerValidator : AbstractValidator<UpdateWorkerCommand>
{
    public UpdateWorkerValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Phone).NotEmpty();
    }
}
