using Xizzmat.SR.Application.Abstractions.Messaging;

namespace Xizzmat.SR.Application.Features.ServiceRequests.Commands.CompleteService;

public record CompleteServiceRequestCommand(
    Guid ServiceId) : ICommand;
