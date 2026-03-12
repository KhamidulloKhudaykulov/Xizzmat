using FluentValidation;
using Xizzmat.Worker.Application.Abstractions.Messaging;
using Xizzmat.Worker.Application.Features.Workers.Commands.AddLocation;
using Xizzmat.Worker.Domain.Repositories;
using Xizzmat.Worker.Domain.Shared;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.AddReview;

public class AddReviewCommandHandler : ICommandHandler<AddReviewCommand>
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IWorkerReviewRepository _workerReviewRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddReviewCommand> _validator;

    public AddReviewCommandHandler(
        IWorkerReviewRepository workerReviewRepository,
        IUnitOfWork unitOfWork,
        IValidator<AddReviewCommand> validator,
        IWorkerRepository workerRepository)
    {
        _workerReviewRepository = workerReviewRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _workerRepository = workerRepository;
    }
    public async Task<Result> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return Result.Failure(new Error(
                ResultStatus.ValidationError,
                validationResult.Errors.First().ErrorMessage));

        var existingWorker = await _workerRepository.SelectAsync(w => w.Id == request.WorkerId);
        if (existingWorker is null)
            return Result.Failure(new Error(ResultStatus.NotFound, "Worker not found"));

        existingWorker.Value.AddReview(request.Rating, request.Comment ?? null);
        await _workerReviewRepository.InsertAsync(existingWorker.Value.Reviews.Last());
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}