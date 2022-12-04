namespace ThisRefactored.Domain;

public interface IEntity
{
    public IEnumerable<IDomainEvent> DomainEvents { get; }
    public void ClearEvents();
}