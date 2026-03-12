using Xizzmat.SR.Application.Abstractions.Messaging;

namespace Xizzmat.SR.Application.Features.ServiceRequests.Commands.CreateService;

public record CreateServiceRequestCommand(
    Guid CustomerId,
    Guid WorkerId) : ICommand<Guid>;
