using Xizzmat.Worker.Application.Abstractions.Messaging;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.CreateWorker;

public record CreateWorkerCommand(
    string Name,
    string Surname,
    string Phone,
    string? Email,
    string City
) : ICommand<Guid>;
