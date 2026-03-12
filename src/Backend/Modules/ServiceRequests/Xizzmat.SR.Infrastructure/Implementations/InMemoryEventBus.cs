using Xizzmat.SR.Application.Interfaces;
using Xizzmat.SR.Domain.Primitives;

namespace Xizzmat.SR.Infrastructure.Implementations;

public class InMemoryEventBus : IEventBus
{
    private readonly Dictionary<Type, List<Func<DomainEvent, Task>>> _handlers = new();

    public Task Publish<TEvent>(TEvent @event) where TEvent : DomainEvent
    {
        var eventType = typeof(TEvent);

        if (_handlers.ContainsKey(eventType))
        {
            var tasks = _handlers[eventType].Select(h => h(@event));
            return Task.WhenAll(tasks);
        }

        return Task.CompletedTask;
    }

    public void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : DomainEvent
    {
        var eventType = typeof(TEvent); 

        if (!_handlers.ContainsKey(eventType))
            _handlers[eventType] = new List<Func<DomainEvent, Task>>();

        _handlers[eventType].Add(e => handler((TEvent)e));
    }
}
