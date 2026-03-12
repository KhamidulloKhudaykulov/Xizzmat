using Xizzmat.Worker.Domain.Entities;

namespace Xizzmat.Worker.Domain.Specifications;

public static class WorkerSpecifications
{
    public static ISpecification<WorkerEntity> ById(Guid id) => new ByIdSpecification(id);
    public static ISpecification<WorkerEntity> ByDistrict(string city, string district) => new WorkersByDistrictSpecification(city, district);
    public static ISpecification<WorkerEntity> Filter(
        string? City = null,
        string? District = null,
        string? Skill = null,
        double? MinRating = null,
        double? MaxRating = null,
        int PageNumber = 1,
        int PageSize = 10) => new WorkerFilterSpecification(City, District, Skill, MinRating, MaxRating, PageNumber, PageSize);

    private sealed class ByIdSpecification : Specification<WorkerEntity>
    {
        public ByIdSpecification(Guid id)
        {
            Criteria = c => c.Id == id;
            AsNoTracking = true;

            Includes.Add(c => c.Locations);
            Includes.Add(c => c.Skills);
            Includes.Add(c => c.Services);
            Includes.Add(c => c.Reviews);
        }
    }

    private sealed class WorkersByDistrictSpecification : Specification<WorkerEntity>
    {
        public WorkersByDistrictSpecification(string city, string district)
        {
            Criteria = c => c.Locations.Any(l => string.Equals(l.City, city) && string.Equals(l.District, district)) && c.IsActive;
            AsNoTracking = true;

            Includes.Add(c => c.Locations);
            Includes.Add(c => c.Skills);
            Includes.Add(c => c.Services);
            Includes.Add(c => c.Reviews);
        }
    }

    private sealed class WorkerFilterSpecification : Specification<WorkerEntity>
    {
        public WorkerFilterSpecification(
            string? City,
            string? District,
            string? Skill,
            double? MinRating,
            double? MaxRating,
            int PageNumber = 1,
            int PageSize = 10)
        {
            Criteria = w =>
                (string.IsNullOrEmpty(City) || w.Locations.Any(l => l.City == City)) &&
                (string.IsNullOrEmpty(District) || w.Locations.Any(l => l.District == District)) &&
                (string.IsNullOrEmpty(Skill) || w.Skills.Any(s => s.Name == Skill)) &&
                (!MinRating.HasValue || w.Rating >= MinRating.Value) &&
                (!MaxRating.HasValue || w.Rating <= MaxRating.Value);

            OrderBy = q => q.OrderByDescending(w => w.Rating);
            Skip = Math.Max((PageNumber - 1) * PageSize, 0);
            Take = PageSize > 0 ? PageSize : 10;
            AsNoTracking = true;

            Includes.Add(w => w.Skills);
            Includes.Add(w => w.Locations);
            Includes.Add(w => w.Services);
            Includes.Add(w => w.Reviews);
        }
    }
}
