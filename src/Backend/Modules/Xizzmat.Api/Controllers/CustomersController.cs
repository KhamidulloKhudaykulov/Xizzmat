using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xizzmat.Customer.Application.Features.Customers.Commands.CreateCustomer;
using Xizzmat.Customer.Application.Features.Customers.Commands.UpdateProfile;
using Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomerByPhone;
using Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomerProfile;
using Xizzmat.Customer.Application.Features.Customers.Queries.GetDeletedCustomers;
using Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomers;
using Xizzmat.Customer.Application.Features.Customers.Queries.GetCustomersCount;
using Xizzmat.Customer.Application.Features.Customers.Queries.GetRecentlyRegistered;
using Xizzmat.Customer.Application.Features.Customers.Queries.SearchCustomers;
using Xizzmat.Customer.Application.Features.Customers.Commands.DeleteCustomer;
using Xizzmat.Customer.Application.Features.Customers.Commands.RestoreCustomer;
using Xizzmat.Customer.Application.Features.Customers.Commands.HardDeleteCustomer;
using Xizzmat.Customer.Application.Features.Customers.Queries;

namespace Xizzmat.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : BaseController
{
    private readonly ISender _sender;

    public CustomersController(ISender sender, IHttpContextAccessor _httpContext)
        : base(_httpContext)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
    {
        var result = await _sender.Send(command);
        return ApiResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var result = await _sender.Send(new GetCustomerByIdQuery(id));
        return ApiResult(result);
    }

    [HttpGet("by-phone/{phone}")]
    public async Task<IActionResult> GetByPhone(string phone)
    {
        var result = await _sender.Send(new GetCustomerByPhoneQuery(phone));
        return ApiResult(result);
    }

    [HttpGet("recently-registered")]
    public async Task<IActionResult> GetRecentlyRegistered([FromQuery] int days = 1, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _sender.Send(new GetRecentlyRegisteredCustomersQuery(days, page, pageSize));
        return ApiResult(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string term = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        if (string.IsNullOrWhiteSpace(term)) return BadRequest("Search term is required.");
        var result = await _sender.Send(new SearchCustomersQuery(term, page, pageSize));
        return ApiResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _sender.Send(new GetCustomersQuery(page, pageSize));
        return ApiResult(result);
    }

    [HttpGet("{id:guid}/profile")]
    public async Task<IActionResult> GetProfile(Guid id)
    {
        var result = await _sender.Send(new GetCustomerProfileQuery(id));
        return ApiResult(result);
    }

    [HttpGet("deleted")]
    public async Task<IActionResult> GetDeleted([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _sender.Send(new GetDeletedCustomersQuery(page, pageSize));
        return ApiResult(result);
    }

    [HttpGet("count")]
    public async Task<IActionResult> Count()
    {
        var result = await _sender.Send(new GetCustomersCountQuery());
        return ApiResult(result);
    }

    [HttpPut("{id:guid}/profile")]
    public async Task<IActionResult> UpdateProfile(Guid id, UpdateProfileCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("The ID in the URL does not match the ID in the request body.");
        }
        var result = await _sender.Send(command);
        return ApiResult(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        var result = await _sender.Send(new DeleteCustomerCommand(id));
        return ApiResult(result);
    }

    [HttpPost("{id:guid}/restore")]
    public async Task<IActionResult> RestoreCustomer(Guid id)
    {
        var result = await _sender.Send(new RestoreCustomerCommand(id));
        return ApiResult(result);
    }

    [HttpDelete("{id:guid}/hard")]
    public async Task<IActionResult> HardDeleteCustomer(Guid id)
    {
        var result = await _sender.Send(new HardDeleteCustomerCommand(id));
        return ApiResult(result);
    }
}
