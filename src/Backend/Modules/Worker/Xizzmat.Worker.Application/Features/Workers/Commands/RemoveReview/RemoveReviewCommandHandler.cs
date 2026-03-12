using FluentValidation;
using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.RemoveReview;

public class RemoveReviewCommandHandler : ICommandHandler<RemoveReviewCommand>
{
    private readonly IWorkerReviewRepository _workerReviewRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IValidator<RemoveReviewCommand> _validator;

    public RemoveReviewCommandHandler(IUnitOfWork unitOfWork, IWorkerReviewRepository workerReviewRepository, IValidator<RemoveReviewCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _workerReviewRepository = workerReviewRepository;
        _validator = validator;
    }

    public async Task<Result> Handle(RemoveReviewCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Failure(new Error(ResultStatus.ValidationError, validationResult.Errors.First().ErrorMessage));

        var existingReview = await _workerReviewRepository.SelectAsync(r => r.Id == request.ReviewId);
        if (existingReview is null)
            return Result.Failure(new Error(ResultStatus.NotFound, "Review not found"));

        await _workerReviewRepository.DeleteAsync(existingReview.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
