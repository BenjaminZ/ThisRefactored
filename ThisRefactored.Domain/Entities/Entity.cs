using System.ComponentModel.DataAnnotations.Schema;

namespace ThisRefactored.Domain.Entities;

public abstract class Entity : IEntity
{
    // using list is okay here since there is only one unit of work per request
    [NotMapped]
    private readonly List<IDomainEvent> _domainEvents = new();

    [NotMapped]
    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }
    protected void PublishDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    // here can be improved with sortable uuid using MassTransient
    protected static Guid NewIdGuid() => Guid.NewGuid();
}