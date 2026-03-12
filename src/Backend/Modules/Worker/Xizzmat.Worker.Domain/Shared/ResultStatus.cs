namespace Xizzmat.Worker.Domain.Shared;

public enum ResultStatus
{
    Success = 200,
    ValidationError = 400,
    NotFound = 404,
    Conflict = 409,
    ServerError = 500
}
