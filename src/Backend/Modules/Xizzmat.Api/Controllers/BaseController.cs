using Microsoft.AspNetCore.Mvc;
using Xizzmat.Customer.Domain.Shared;

public abstract class BaseController : ControllerBase
{
    protected IHttpContextAccessor HttpContextAccessor { get; }

    protected BaseController(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }

    protected IActionResult ApiResult(Result r)
    {
        // If the result is a generic Result<T> and successful, return the value
        var resultType = r.GetType();
        if (resultType.IsGenericType && r.IsSuccess)
        {
            var value = ((dynamic)r).Value;
            return Ok(value);
        }

        switch (r.Error?.Code)
        {
            case ResultStatus.Success:
                return Ok();

            case ResultStatus.ValidationError:
                return StatusCode((int)ResultStatus.ValidationError, r.Error.Message);

            case ResultStatus.NotFound:
                return StatusCode((int)ResultStatus.NotFound, r.Error.Message);

            case ResultStatus.Conflict:
                return StatusCode((int)ResultStatus.Conflict, r.Error.Message);

            case ResultStatus.ServerError:
                return StatusCode((int)ResultStatus.ServerError, r.Error.Message);

            default:
                return StatusCode(500, "An unexpected error occurred.");
        }
    }
}
