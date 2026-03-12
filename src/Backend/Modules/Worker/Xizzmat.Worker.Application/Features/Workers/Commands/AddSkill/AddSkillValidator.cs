using FluentValidation;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.AddSkill;

public class AddSkillValidator : AbstractValidator<AddSkillCommand>
{
    public AddSkillValidator()
    {
        RuleFor(x => x.WorkerId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}
