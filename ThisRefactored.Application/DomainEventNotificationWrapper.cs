using MediatR;
using ThisRefactored.Domain;

namespace ThisRefactored.Application;

public class DomainEventNotificationWrapper<T> : INotification where T : IDomainEvent
{
    public T DomainEvent { get; }

    public DomainEventNotificationWrapper(T domainEvent)
    {
        DomainEvent = domainEvent;
    } 
}