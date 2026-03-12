using Xizzmat.SR.Domain.Enums;
using Xizzmat.SR.Domain.Events;
using Xizzmat.SR.Domain.Primitives;
using Xizzmat.SR.Domain.Shared;

namespace Xizzmat.SR.Domain.Entities;

public class ServiceRequest : AggregateRoot
{
    public Guid CustomerId { get; private set; }
    public Guid WorkerId { get; private set; }
    public ServiceStatus Status { get; private set; }

    private ServiceRequest(Guid customerId, Guid workerId)
    {
        CustomerId = customerId;
        WorkerId = workerId;
        Status = ServiceStatus.Created;
    }

    public static Result<ServiceRequest> Create(Guid customerId, Guid workerId)
    {
        var service = new ServiceRequest(customerId, workerId);
        service.AddDomainEvent(new ServiceCreatedEvent(service.Id, customerId));
        return service;
    }
    public Result Accept(Guid workerId)
    {
        if (Status != ServiceStatus.Created)
            return Result.Failure(new Error(ResultStatus.Conflict, "Service already accepted"));

        WorkerId = workerId;
        Status = ServiceStatus.Accepted;

        AddDomainEvent(new ServiceAcceptedEvent(Id, WorkerId, CustomerId));

        return Result.Success();
    }

    public Result Start()
    {
        if (Status != ServiceStatus.Accepted)
            return Result.Failure(new Error(ResultStatus.Conflict, "Service cannot start"));

        Status = ServiceStatus.InProgress;
        AddDomainEvent(new ServiceStartedEvent(Id, WorkerId, CustomerId));
        return Result.Success();
    }

    public Result Complete()
    {
        if (Status != ServiceStatus.InProgress)
            return Result.Failure(new Error(ResultStatus.Conflict, "Service not in progress"));

        Status = ServiceStatus.Completed;
        AddDomainEvent(new ServiceCompletedEvent(Id, WorkerId, CustomerId));
        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status == ServiceStatus.Completed)
            return Result.Failure(new Error(ResultStatus.Conflict, "Cannot cancel completed service"));

        Status = ServiceStatus.Cancelled;
        AddDomainEvent(new ServiceCancelledEvent(Id, CustomerId));
        return Result.Success();
    }
}