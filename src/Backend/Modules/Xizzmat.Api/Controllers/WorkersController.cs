using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xizzmat.Worker.Application.Features.Workers.Commands.AddLocation;
using Xizzmat.Worker.Application.Features.Workers.Commands.AddReview;
using Xizzmat.Worker.Application.Features.Workers.Commands.AddService;
using Xizzmat.Worker.Application.Features.Workers.Commands.AddSkill;
using Xizzmat.Worker.Application.Features.Workers.Commands.CreateWorker;
using Xizzmat.Worker.Application.Features.Workers.Commands.DeleteWorker;
using Xizzmat.Worker.Application.Features.Workers.Commands.RemoveReview;
using Xizzmat.Worker.Application.Features.Workers.Commands.UpdateWorker;
using Xizzmat.Worker.Application.Features.Workers.Queries.GetWorkerById;
using Xizzmat.Worker.Application.Features.Workers.Queries.GetWorkers;
using Xizzmat.Worker.Application.Features.Workers.Queries.GetWorkersByDistrict;

namespace Xizzmat.Api.Controllers;

[ApiController]
[Route("api/workers")]
public class WorkersController : BaseController
{
    private readonly ISender _sender;

    public WorkersController(ISender sender, IHttpContextAccessor _httpContext)
        : base(_httpContext)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorker(CreateWorkerCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error.Message);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateWorker(Guid id, UpdateWorkerCommand command)
    {
        if (id != command.Id)
            return BadRequest("The ID in the URL does not match the ID in the request body.");

        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok();

        return BadRequest(result.Error.Message);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteWorker(Guid id)
    {
        var result = await _sender.Send(new DeleteWorkerCommand(id));
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result.Error.Message);
    }

    [HttpPost("{id:guid}/skills")]
    public async Task<IActionResult> AddSkill(Guid id, AddSkillCommand command)
    {
        if (id != command.WorkerId)
            return BadRequest("The ID in the URL does not match the workerId in the request body.");

        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result.Error.Message);
    }

    [HttpPost("{id:guid}/locations")]
    public async Task<IActionResult> AddLocation(Guid id, AddLocationCommand command)
    {
        if (id != command.WorkerId)
            return BadRequest("The ID in the URL does not match the workerId in the request body.");

        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result.Error.Message);
    }

    [HttpPost("{id:guid}/services")]
    public async Task<IActionResult> AddService(Guid id, AddServiceCommand command)
    {
        if (id != command.WorkerId)
            return BadRequest("The ID in the URL does not match the workerId in the request body.");

        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result.Error.Message);
    }

    [HttpPost("{id:guid}/reviews")]
    public async Task<IActionResult> AddReview(Guid id, AddReviewCommand command)
    {
        if (id != command.WorkerId)
            return BadRequest("The ID in the URL does not match the workerId in the request body.");

        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result.Error.Message);
    }

    [HttpDelete("{id:guid}/reviews")]
    public async Task<IActionResult> RemoveReview(Guid id, RemoveReviewCommand command)
    {
        if (id != command.ReviewId)
            return BadRequest("The ID in the URL does not match the reviewId in the request body.");

        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result.Error.Message);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetWorkerByIdQuery(id);

        var result = await _sender.Send(query);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result.Error.Message);
    }

    [HttpGet("by-district")]
    public async Task<IActionResult> GetByDistrict(
        [FromQuery] string city,
        [FromQuery] string district,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetWorkersByDistrictQuery(
            city,
            district,
            pageNumber,
            pageSize
        );

        var result = await _sender.Send(query);

        if (result.IsFailure)
            return BadRequest(result.Error.Message);

        return Ok(result.Value);
    }

    [HttpGet("by-filter")]
    public async Task<IActionResult> GetByFilter(
        [FromQuery] GetWorkersQuery query)
    {
        var result = await _sender.Send(query);
        if (result.IsSuccess) return Ok(result.Value);

        return BadRequest(result.Error.Message);
    }
}
