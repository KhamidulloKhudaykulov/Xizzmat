using FluentValidation;

namespace Xizzmat.SR.Application.Features.ServiceRequests.Commands.CompleteService;

public class CompleteServiceRequestValidator : AbstractValidator<CompleteServiceRequestCommand>
{
    public CompleteServiceRequestValidator()
    {
        RuleFor(x => x.ServiceId).NotEmpty().WithMessage("ServiceId is required");
    }
}
