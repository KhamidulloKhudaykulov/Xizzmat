namespace Xizzmat.Worker.Application.Interfaces;

public interface IWorkerService
{
    Task<bool> ExistsAsync(Guid id);
}
