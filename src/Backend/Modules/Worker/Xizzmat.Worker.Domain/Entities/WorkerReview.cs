namespace Xizzmat.Worker.Domain.Entities;

public class WorkerReview
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public virtual WorkerEntity? Worker { get; set; }

    public WorkerReview() { }

    public WorkerReview(Guid id, Guid workerId, int rating, string? comment)
    {
        Id = id;
        WorkerId = workerId;
        Rating = rating;
        Comment = comment;
        CreatedAt = DateTime.UtcNow;
    }
}
