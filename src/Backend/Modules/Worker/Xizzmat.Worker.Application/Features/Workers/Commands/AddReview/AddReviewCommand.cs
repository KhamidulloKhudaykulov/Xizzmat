using Xizzmat.Worker.Application.Abstractions.Messaging;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.AddReview;

public record AddReviewCommand(Guid WorkerId, int Rating, string? Comment) : ICommand;