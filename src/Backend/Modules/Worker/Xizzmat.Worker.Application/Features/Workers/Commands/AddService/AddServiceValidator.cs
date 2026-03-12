using FluentValidation;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.AddService;

public class AddServiceValidator : AbstractValidator<AddServiceCommand>
{
    public AddServiceValidator()
    {
        RuleFor(x => x.WorkerId)
            .NotEmpty().WithMessage("WorkerId is required.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("ServiceName is required.")
            .MaximumLength(100).WithMessage("ServiceName must not exceed 100 characters.");
    }
}
