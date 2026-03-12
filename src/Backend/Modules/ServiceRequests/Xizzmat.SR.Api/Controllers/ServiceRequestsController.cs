using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xizzmat.SR.Application.Features.ServiceRequests.Commands.CreateService;
using Xizzmat.SR.Application.Features.ServiceRequests.Commands.AcceptService;
using Xizzmat.SR.Application.Features.ServiceRequests.Commands.CompleteService;
using Xizzmat.SR.Application.Features.ServiceRequests.Commands.CancelService;

namespace Xizzmat.SR.Api.Controllers;

[ApiController]
[Route("api/service-requests")]
public class ServiceRequestsController : ControllerBase
{
    private readonly ISender _sender;

    public ServiceRequestsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateService(CreateServiceRequestCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess ? CreatedAtAction(null, new { id = result.Value }) : BadRequest(result.Error.Message);
    }

    [HttpPost("{id:guid}/accept")]
    public async Task<IActionResult> AcceptService(Guid id, AcceptServiceRequestCommand command)
    {
        if (id != command.ServiceId) return BadRequest("Service id mismatch");
        var result = await _sender.Send(command);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error.Message);
    }

    [HttpPost("{id:guid}/start")]
    public async Task<IActionResult> StartService(Guid id, CompleteServiceRequestCommand command)
    {
        if (id != command.ServiceId) return BadRequest("Service id mismatch");
        var result = await _sender.Send(command);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error.Message);
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> CancelService(Guid id, CancelServiceRequestCommand command)
    {
        if (id != command.ServiceId) return BadRequest("Service id mismatch");
        var result = await _sender.Send(command);
        return result.IsSuccess ? NoContent() : BadRequest(result.Error.Message);
    }
}
