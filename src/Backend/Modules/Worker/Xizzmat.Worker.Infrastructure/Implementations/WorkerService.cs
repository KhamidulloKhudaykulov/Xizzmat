using Microsoft.EntityFrameworkCore;
using Xizzmat.Worker.Application.Interfaces;
using Xizzmat.Worker.Infrastructure.Database;

namespace Xizzmat.Worker.Infrastructure.Implementations;

public class WorkerService : IWorkerService
{
    private readonly WorkerDbContext _context;

    public WorkerService(WorkerDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context
            .Workers
            .FirstOrDefaultAsync(x => x.Id == id) != null;
    }
}
