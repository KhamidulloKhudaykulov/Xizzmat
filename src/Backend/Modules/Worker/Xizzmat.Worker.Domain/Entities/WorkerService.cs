namespace Xizzmat.Worker.Domain.Entities;

public class WorkerService
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }

    // Navigation
    public virtual WorkerEntity? Worker { get; set; }

    public WorkerService() { }

    public WorkerService(Guid id, Guid workerId, string name, decimal price)
    {
        Id = id;
        WorkerId = workerId;
        Name = name;
        Price = price;
    }
}
