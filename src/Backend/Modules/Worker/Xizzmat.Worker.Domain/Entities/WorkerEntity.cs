using Xizzmat.Worker.Domain.Events;
using Xizzmat.Worker.Domain.Primitives;
using Xizzmat.Worker.Domain.Shared;

namespace Xizzmat.Worker.Domain.Entities;

public class WorkerEntity : AggregateRoot
{
    public string Name { get; private set; } = default!;
    public string Surname { get; private set; } = default!;
    public string Phone { get; private set; } = default!;
    public string? Email { get; private set; }
    // Primary city and additional locations a worker can serve
    public string City { get; private set; } = default!;
    // A worker can serve multiple locations (city + district)
    public virtual ICollection<WorkerLocation> Locations { get; set; } = new List<WorkerLocation>();
    
    public double Rating { get; private set; } = 0.0;
    public bool IsActive { get; set; } = true;

    // Relations
    public virtual ICollection<WorkerSkill> Skills { get; set; } = new List<WorkerSkill>();
    public virtual ICollection<WorkerService> Services { get; set; } = new List<WorkerService>();
    public virtual ICollection<WorkerReview> Reviews { get; set; } = new List<WorkerReview>();

    private WorkerEntity() { }

    private WorkerEntity(Guid id, string name, string surname, string phone, string? email, string city)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;

        Name = name;
        Surname = surname;
        Phone = phone;
        Email = email;
        City = city;
        IsActive = true;
    }

    public Result AddLocation(string city, string district)
    {
        if (string.IsNullOrWhiteSpace(city) && string.IsNullOrWhiteSpace(district))
            return Result.Failure(new Error(ResultStatus.ValidationError, "City or district is required"));

        if (Locations.Any(l => string.Equals(l.City, city, StringComparison.OrdinalIgnoreCase) && string.Equals(l.District, district, StringComparison.OrdinalIgnoreCase)))
            return Result.Failure(new Error(ResultStatus.Conflict, "Location already added to worker"));

        Locations.Add(new WorkerLocation(Guid.NewGuid(), Id, city?.Trim() ?? string.Empty, district?.Trim() ?? string.Empty));
        return Result.Success();
    }

    public Result RemoveLocation(Guid locationId)
    {
        var loc = Locations.FirstOrDefault(l => l.Id == locationId);
        if (loc is null)
            return Result.Failure(new Error(ResultStatus.NotFound, "Location not found"));

        Locations.Remove(loc);
        return Result.Success();
    }

    public static Result<WorkerEntity> Create(Guid id, string name, string surname, string phone, string? email, string city)
    {
        var worker = new WorkerEntity(id == Guid.Empty ? Guid.NewGuid() : id, name, surname, phone, email, city);
        worker.AddDomainEvent(new WorkerCreated(worker.Id));
        return Result.Success(worker);
    }

    public Result AddSkill(string name)
    {
        if (Skills.Any(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase)))
            return Result.Failure(new Error(ResultStatus.Conflict, "Skill already exists for worker"));

        var skill = new WorkerSkill(Guid.NewGuid(), Id, name.Trim());
        Skills.Add(skill);
        AddDomainEvent(new SkillAdded(Id, skill.Id, skill.Name));
        return Result.Success();
    }

    public Result RemoveSkill(Guid skillId)
    {
        var skill = Skills.FirstOrDefault(s => s.Id == skillId);
        if (skill is null)
            return Result.Failure(new Error(ResultStatus.NotFound, "Skill not found"));

        Skills.Remove(skill);
        AddDomainEvent(new SkillRemoved(Id, skill.Id));
        return Result.Success();
    }

    public Result AddService(string name, decimal price)
    {
        if (Services.Any(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase)))
            return Result.Failure(new Error(ResultStatus.Conflict, "Service already added to worker"));

        var svc = new WorkerService(Guid.NewGuid(), Id, name.Trim(), price);
        Services.Add(svc);
        AddDomainEvent(new ServiceAdded(Id, svc.Id, svc.Name, svc.Price));
        return Result.Success();
    }

    public Result RemoveService(Guid serviceId)
    {
        var service = Services.FirstOrDefault(s => s.Id == serviceId);
        if (service is null)
            return Result.Failure(new Error(ResultStatus.NotFound, "Service not found"));

        Services.Remove(service);
        AddDomainEvent(new ServiceRemoved(Id, service.Id));
        return Result.Success();
    }

    public Result AddReview(int rating, string? comment = null)
    {
        var rev = new WorkerReview(Guid.NewGuid(), Id, rating, comment);
        Reviews.Add(rev);
        RecalculateRating();
        AddDomainEvent(new ReviewAdded(Id, rev.Id, rev.Rating, rev.Comment));
        return Result.Success();
    }

    private void RecalculateRating()
    {
        if (!Reviews.Any())
        {
            Rating = 0.0;
            return;
        }

        Rating = Math.Round(Reviews.Average(r => r.Rating), 2);
    }
}
