using FluentValidation;
using Xizzmat.Customer.Application.Interfaces;
using Xizzmat.Worker.Application.Interfaces;

namespace Xizzmat.SR.Application.Features.ServiceRequests.Commands.CreateService;

public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequestCommand>
{
    private readonly ICustomerService _customerService;
    private readonly IWorkerService _workerService;
    public CreateServiceRequestValidator(ICustomerService customerService, IWorkerService workerService)
    {
        _customerService = customerService;
        _workerService = workerService;

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required")
            .MustAsync(CustomerExists).WithMessage("Customer does not exist");

        RuleFor(x => x.WorkerId)
            .NotEmpty().WithMessage("WorkerId is required")
            .MustAsync(WorkerExists).WithMessage("Worker does not exist");
    }

    private async Task<bool> CustomerExists(Guid id, CancellationToken ct)
    {
        return await _customerService.ExistsAsync(id);
    }

    private async Task<bool> WorkerExists(Guid id, CancellationToken ct)
    {
        return await _workerService.ExistsAsync(id);
    }
}
