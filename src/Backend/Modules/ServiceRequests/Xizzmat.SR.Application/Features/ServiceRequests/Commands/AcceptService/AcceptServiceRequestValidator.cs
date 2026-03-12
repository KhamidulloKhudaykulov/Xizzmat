using FluentValidation;
using Xizzmat.Worker.Application.Interfaces;

namespace Xizzmat.SR.Application.Features.ServiceRequests.Commands.AcceptService
{
    public class AcceptServiceRequestValidator : AbstractValidator<AcceptServiceRequestCommand>
    {
        private readonly IWorkerService _workerService;
        public AcceptServiceRequestValidator(IWorkerService workerService)
        {
            _workerService = workerService;

            RuleFor(x => x.ServiceId).NotEmpty().WithMessage("ServiceId is required");

            RuleFor(x => x.WorkerId)
                .NotEmpty()
                .WithMessage("WorkerId is required")
                .MustAsync(WorkerExists)
                .WithMessage("Worker does not exist");
        }

        private async Task<bool> WorkerExists(Guid id, CancellationToken ct)
        {
            return await _workerService.ExistsAsync(id);
        }
    }
}
