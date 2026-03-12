using Xizzmat.Worker.Application.Abstractions.Messaging;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.AddService;

public record AddServiceCommand(Guid WorkerId, string Name, decimal Price) : ICommand;