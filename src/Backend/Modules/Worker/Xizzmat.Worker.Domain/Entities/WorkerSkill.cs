namespace Xizzmat.Worker.Domain.Entities;

public class WorkerSkill
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }
    public string Name { get; set; } = default!;

    // Navigation
    public virtual WorkerEntity? Worker { get; set; }

    public WorkerSkill() { }

    public WorkerSkill(Guid id, Guid workerId, string name)
    {
        Id = id;
        WorkerId = workerId;
        Name = name;
    }
}
