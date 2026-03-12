using Xizzmat.SR.Domain.Primitives;

namespace Xizzmat.SR.Application.Interfaces;

public interface IEventBus
{
    Task Publish<TEvent>(TEvent @event) where TEvent : DomainEvent;
}
