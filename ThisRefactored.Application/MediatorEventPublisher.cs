using MediatR;
using ThisRefactored.Domain;

namespace ThisRefactored.Application;

public class MediatorEventPublisher : IDomainEventPublisher
{
    private readonly IMediator _mediator;

    public MediatorEventPublisher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishDomainEventAsync<T>(T domainEvent, CancellationToken cancellationToken = default)
        where T : IDomainEvent
    {
        var notification = CreateDomainEventNotification(domainEvent);
        await _mediator.Publish(notification, cancellationToken);
    }

    // Need to use reflection here to enforce the domain event concrete type
    private static INotification CreateDomainEventNotification(IDomainEvent domainEvent)
    {
        var genericDispatcherType = typeof(DomainEventNotificationWrapper<>).MakeGenericType(domainEvent.GetType());
        return (INotification)(Activator.CreateInstance(genericDispatcherType, domainEvent) ?? throw new Exception("Could not create domain event notification"));
    }
}