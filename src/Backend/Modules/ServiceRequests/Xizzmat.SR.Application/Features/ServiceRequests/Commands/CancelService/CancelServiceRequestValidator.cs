using FluentValidation;

namespace Xizzmat.SR.Application.Features.ServiceRequests.Commands.CancelService;

public class CancelServiceRequestValidator : AbstractValidator<CancelServiceRequestCommand>
{
    public CancelServiceRequestValidator()
    {
        RuleFor(x => x.ServiceId).NotEmpty().WithMessage("ServiceId is required");
    }
}
