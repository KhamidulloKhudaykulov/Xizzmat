using Xizzmat.SR.Application.Abstractions.Messaging;

namespace Xizzmat.SR.Application.Features.ServiceRequests.Commands.CancelService;

public record CancelServiceRequestCommand(
    Guid ServiceId) : ICommand;
