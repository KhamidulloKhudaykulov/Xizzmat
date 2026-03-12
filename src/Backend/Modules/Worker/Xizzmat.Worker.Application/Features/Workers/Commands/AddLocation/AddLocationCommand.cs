using Xizzmat.Worker.Application.Abstractions.Messaging;

namespace Xizzmat.Worker.Application.Features.Workers.Commands.AddLocation;

public record AddLocationCommand(Guid WorkerId, string City, string District) : ICommand;