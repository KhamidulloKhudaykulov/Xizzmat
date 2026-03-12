using Xizzmat.Worker.Application.Abstractions.Messaging;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.DeleteWorker;

public record DeleteWorkerCommand(Guid Id) : ICommand;
