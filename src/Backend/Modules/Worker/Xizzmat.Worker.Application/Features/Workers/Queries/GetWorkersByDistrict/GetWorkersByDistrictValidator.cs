using FluentValidation;

namespace Xizzmat.Worker.Application.Features.Workers.Queries.GetWorkersByDistrict;

public class GetWorkersByDistrictValidator : AbstractValidator<GetWorkersByDistrictQuery>
{
    public GetWorkersByDistrictValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.District)
            .NotEmpty().WithMessage("District is required.");

        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be >= 1");
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).WithMessage("PageSize must be >= 1");
    }
}
