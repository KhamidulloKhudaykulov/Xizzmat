using Xizzmat.Worker.Application.Abstractions.Messaging;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.RemoveReview;

public record RemoveReviewCommand(Guid ReviewId) : ICommand;
