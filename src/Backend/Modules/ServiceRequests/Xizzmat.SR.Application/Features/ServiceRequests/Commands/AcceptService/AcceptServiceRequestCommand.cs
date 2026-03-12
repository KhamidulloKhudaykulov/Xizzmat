using Xizzmat.SR.Application.Abstractions.Messaging;

namespace Xizzmat.SR.Application.Features.ServiceRequests.Commands.AcceptService;

public record AcceptServiceRequestCommand(
    Guid ServiceId,
    Guid WorkerId) : ICommand;
