namespace Xizzmat.Worker.Domain.Entities;

public class WorkerLocation
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }
    public string City { get; set; } = default!;
    public string District { get; set; } = default!;

    public virtual WorkerEntity? Worker { get; set; }

    public WorkerLocation() { }

    public WorkerLocation(Guid id, Guid workerId, string city, string district)
    {
        Id = id;
        WorkerId = workerId;
        City = city;
        District = district;
    }
}
