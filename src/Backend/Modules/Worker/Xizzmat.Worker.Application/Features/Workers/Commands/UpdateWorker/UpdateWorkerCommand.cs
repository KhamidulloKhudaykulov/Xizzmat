using Xizzmat.Worker.Application.Abstractions.Messaging;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.UpdateWorker;

public record UpdateWorkerCommand(
    Guid Id,
    string Name,
    string Surname,
    string Phone,
    string? Email,
    string City
) : ICommand;
