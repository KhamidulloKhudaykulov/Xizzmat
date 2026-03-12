using FluentValidation;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.RemoveReview;

public class RemoveReviewValidator : AbstractValidator<RemoveReviewCommand>
{
    public RemoveReviewValidator()
    {
        RuleFor(x => x.ReviewId)
            .NotEmpty().WithMessage("WorkerId is required.");
    }
}
