namespace ThisRefactored.Domain;

public interface IDomainEventPublisher
{
    public Task PublishDomainEventAsync<T>(T domainEvent, CancellationToken cancellationToken = default)
        where T : IDomainEvent;
}