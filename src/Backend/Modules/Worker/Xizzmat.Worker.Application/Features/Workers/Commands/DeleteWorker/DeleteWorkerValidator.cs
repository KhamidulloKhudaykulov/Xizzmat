using FluentValidation;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.DeleteWorker;

public class DeleteWorkerValidator : AbstractValidator<DeleteWorkerCommand>
{
    public DeleteWorkerValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
